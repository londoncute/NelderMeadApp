using System;
using System.Collections.Generic;
using NCalc;
using NelderMeadWrapper;

namespace Parser
{
    public static class FunctionParser
    {
        public static FunctionPtr ParseFunction(string expression)
        {
            // Нормализация выражения: заменяем проблемные операторы
            string normalizedExpr = NormalizeExpression(expression);

            // Создаем выражение NCalc
            Expression ncalcExpr = new Expression(normalizedExpr, EvaluateOptions.IgnoreCase);

            // Регистрируем стандартные математические функции
            RegisterMathFunctions(ncalcExpr);

            // Проверяем ошибки парсинга
            if (ncalcExpr.HasErrors())
                throw new ArgumentException($"Ошибка в выражении: {ncalcExpr.Error}");

            // Компилируем в делегат
            return (ref Vector2D point) =>
            {
                try
                {
                    ncalcExpr.Parameters["x"] = point.x;
                    ncalcExpr.Parameters["y"] = point.y;
                    object result = ncalcExpr.Evaluate();
                    return Convert.ToDouble(result);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(
                        $"Ошибка вычисления в точке ({point.x}, {point.y}): {ex.Message}");
                }
            };
        }

        private static string NormalizeExpression(string expr)
        {
            // Заменяем оператор '^' на функцию pow() для корректного возведения в степень
            expr = System.Text.RegularExpressions.Regex.Replace(
                expr,
                @"([\d\.xXyYa-zA-Z]+)\s*\^\s*([\d\.xXyYa-zA-Z]+)",
                "pow($1, $2)"
            );

            // Заменяем альтернативные формы операторов
            expr = expr
                .Replace("**", "^") // Двойная звезда -> степень
                .Replace("&&", "and")
                .Replace("||", "or");

            return expr;
        }

        private static void RegisterMathFunctions(Expression expr)
        {
            // Регистрируем математические функции
            expr.EvaluateFunction += (name, args) =>
            {
                switch (name.ToLower())
                {
                    case "pow":
                        if (args.Parameters.Length != 2)
                            throw new ArgumentException("pow требует 2 аргумента");

                        double baseVal = Convert.ToDouble(args.Parameters[0].Evaluate());
                        double expVal = Convert.ToDouble(args.Parameters[1].Evaluate());
                        args.Result = Math.Pow(baseVal, expVal);
                        break;

                    case "sqrt":
                        args.Result = Math.Sqrt(Convert.ToDouble(args.Parameters[0].Evaluate()));
                        break;

                    case "abs":
                        args.Result = Math.Abs(Convert.ToDouble(args.Parameters[0].Evaluate()));
                        break;

                        // Можно добавить другие функции при необходимости
                }
            };

            // Регистрируем константы
            expr.Parameters["pi"] = Math.PI;
            expr.Parameters["e"] = Math.E;
        }
    }
}