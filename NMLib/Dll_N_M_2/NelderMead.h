#pragma once

#include <vector>
#include <array>
#include <functional>
#include <stdexcept>

#ifdef _WIN32
#ifdef NELDERMEAD_EXPORTS
#define NELDERMEAD_API __declspec(dllexport)
#else
#define NELDERMEAD_API __declspec(dllimport)
#endif
#else
#define NELDERMEAD_API
#endif

// 2D vector structure
struct Vector2D {
    double x{ 0.0 };
    double y{ 0.0 };

    Vector2D() = default;
    Vector2D(double x, double y) : x(x), y(y) {}

    // Арифметические операторы
    Vector2D operator+(const Vector2D& other) const { return { x + other.x, y + other.y }; }
    Vector2D operator-(const Vector2D& other) const { return { x - other.x, y - other.y }; }
    Vector2D operator*(double scalar) const { return { x * scalar, y * scalar }; }
    Vector2D operator/(double divisor) const {
        if (divisor == 0.0) throw std::runtime_error("Vector2D division by zero");
        return { x / divisor, y / divisor };
    }

    // Составные операторы присваивания
    Vector2D& operator+=(const Vector2D& other) { x += other.x; y += other.y; return *this; }
    Vector2D& operator-=(const Vector2D& other) { x -= other.x; y -= other.y; return *this; }
    Vector2D& operator*=(double scalar) { x *= scalar; y *= scalar; return *this; }
    Vector2D& operator/=(double divisor) {
        if (divisor == 0.0) throw std::runtime_error("Vector2D division by zero");
        x /= divisor; y /= divisor; return *this;
    }

    // Дополнительные методы
    double squaredNorm() const { return x * x + y * y; }
    double distanceTo(const Vector2D& other) const {
        const double dx = x - other.x;
        const double dy = y - other.y;
        return dx * dx + dy * dy;
    }
};

inline Vector2D operator*(double scalar, const Vector2D& vec) {
    return vec * scalar;
}

struct NelderMeadParams {
    double alpha{ 1.0 };   // Reflection coefficient
    double gamma{ 2.0 };   // Expansion coefficient
    double rho{ 0.5 };     // Contraction coefficient
    double sigma{ 0.5 };   // Shrink coefficient
    double step{ 1.0 };    // Initial simplex step size
    int maxIter{ 1000 };   // Maximum iterations
    double eps{ 1e-6 };    // Convergence threshold
};

struct SimplexPoint {
    Vector2D point;
    double value{ 0.0 };

    bool operator<(const SimplexPoint& other) const {
        return value < other.value;
    }
};

using FunctionPtr = double (*)(const Vector2D&);

class NELDERMEAD_API NelderMead {
public:   
    NelderMead() = default;
    ~NelderMead() = default;

    void setParams(const NelderMeadParams& params) { params_ = params; }
    int optimize(FunctionPtr f, const Vector2D& start, Vector2D& result);
    const std::vector<std::array<SimplexPoint, 3>>& getHistory() const { return history_; }
private:   
    NelderMeadParams params_;
    std::vector<std::array<SimplexPoint, 3>> history_;
    void sortAndPush(std::array<SimplexPoint, 3>& simplex, const FunctionPtr f);
};

extern "C" {
    
    NELDERMEAD_API NelderMead* createNelderMead();
    NELDERMEAD_API void destroyNelderMead(NelderMead* instance);
    NELDERMEAD_API void nelderMeadSetParams(NelderMead* instance, const NelderMeadParams* params); 
    NELDERMEAD_API int nelderMeadOptimize(NelderMead* instance, const Vector2D* initialGuess, FunctionPtr func, Vector2D* result);  
    NELDERMEAD_API void NelderMead_GetHistorySize(NelderMead* instance, int* outCount);
    NELDERMEAD_API void NelderMead_GetHistory(NelderMead* instance, SimplexPoint* outHistory, int* outCount);

}
