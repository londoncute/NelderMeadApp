#include "NelderMead.h"
#include <algorithm>



void NelderMead::sortAndPush(std::array<SimplexPoint, 3>& simplex, FunctionPtr f) {
    for (auto& point : simplex) {
        point.value = f(point.point);
    }
    std::sort(simplex.begin(), simplex.end());
    history_.push_back(simplex);
}

int NelderMead::optimize(const FunctionPtr f, const Vector2D& start, Vector2D& result) {
    if (!f || params_.maxIter <= 0 || params_.eps <= 0.0) {
        return -1;
    }

    history_.clear();
    history_.reserve(params_.maxIter);

    std::array<SimplexPoint, 3> simplex = {
        SimplexPoint{start, 0.0},
        SimplexPoint{{start.x + params_.step, start.y}, 0.0},
        SimplexPoint{{start.x, start.y + params_.step}, 0.0}
    };

    for (int iter = 0; iter < params_.maxIter; ++iter) {
        sortAndPush(simplex, f);

        auto& best = simplex[0];
        auto& second = simplex[1];
        auto& worst = simplex[2];

        try {
            const auto centroid = (best.point + second.point) / 2.0;
            const auto reflected = centroid + (centroid - worst.point) * params_.alpha;
            const double f_ref = f(reflected);

            if (f_ref < best.value) {
                // Expansion
                const auto expanded = centroid + (reflected - centroid) * params_.gamma;
                const double f_exp = f(expanded);
                worst.point = (f_exp < f_ref) ? expanded : reflected;
            }
            else if (f_ref < second.value) {
                // Accept reflection
                worst.point = reflected;
            }
            else {
                // Contraction
                const auto contracted = centroid + (worst.point - centroid) * params_.rho;
                const double f_con = f(contracted);

                if (f_con < worst.value) {
                    worst.point = contracted;
                }
                else {
                    // Shrink
                    second.point = best.point + (second.point - best.point) * params_.sigma;
                    worst.point = best.point + (worst.point - best.point) * params_.sigma;
                }
            }

            // Check convergence
            const double maxDist = std::max({
                best.point.distanceTo(second.point),
                best.point.distanceTo(worst.point),
                second.point.distanceTo(worst.point)
                });

            if (maxDist < params_.eps * params_.eps) {
                break;
            }
        }
        catch (const std::runtime_error&) {
            return -1; // Division by zero occurred
        }
    }

    result = simplex[0].point;
    return 0;
}

void nelderMeadGetHistorySize(NelderMead* instance, int* outCount) {
    if (instance && outCount) {
        *outCount = instance->getHistory().size();
    }
}


extern "C" {
    
    NELDERMEAD_API NelderMead* createNelderMead() {
        return new NelderMead();
    }
    NELDERMEAD_API void destroyNelderMead(NelderMead* instance) {
        if (instance) {
            delete instance;
        }
    }
    NELDERMEAD_API void nelderMeadSetParams(NelderMead* instance, const NelderMeadParams* params) {
        if (instance && params) {
            instance->setParams(*params);
        }
    }
    NELDERMEAD_API int nelderMeadOptimize(NelderMead* instance, const Vector2D* initialGuess, FunctionPtr func, Vector2D* result) {
        if (instance && initialGuess && func && result) {
            return instance->optimize(func, *initialGuess, *result);
        }
        return -1;
    }
    NELDERMEAD_API void NelderMead_GetHistorySize(NelderMead* instance, int* outCount) {
        if (instance && outCount) {
            *outCount = static_cast<int>(instance->getHistory().size());
        }
    }
    NELDERMEAD_API void NelderMead_GetHistory(NelderMead* instance, SimplexPoint* outHistory, int* outCount) {
        if (!instance || !outHistory || !outCount) return;

        const auto& history = instance->getHistory();
        *outCount = static_cast<int>(history.size());

        for (size_t i = 0; i < history.size(); ++i) {
            for (int j = 0; j < 3; ++j) {
                outHistory[i * 3 + j] = history[i][j];
            }
        }
    }
}