using MathNet.Numerics.Optimization;
using System;
using System.Runtime.InteropServices;

namespace NelderMeadWrapper
{
    
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2D
    {
        public double x;
        public double y;

        public Vector2D(double X, double Y)
        {
            x = X;
            y = Y;
        }

        public override string ToString() => $"({x}, {y})";
    }
    // Параметры алгоритма Нелдера-Мида
    [StructLayout(LayoutKind.Sequential)]
    public struct NelderMeadParams
    {
        public double Alpha;   // Reflection coefficient
        public double Gamma;   // Expansion coefficient
        public double Rho;     // Contraction coefficient
        public double Sigma;   // Shrink coefficient
        public double Step;    // Initial simplex step size
        public int MaxIter;    // Maximum iterations
        public double Eps;     // Convergence threshold

        /*public static NelderMeadParams Default => new NelderMeadParams
        {
            Alpha = 1.0,
            Gamma = 2.0,
            Rho = 0.5,
            Sigma = 0.5,
            Step = 1.0,
            MaxIter = 200,
            Eps = 1e-6
        };*/
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct SimplexPoint
    {
        public Vector2D Point;
        public double Value;

        public override string ToString() => $"{Point} (Value: {Value})";
    }


    // Делегат для функции оптимизации
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate double FunctionPtr(ref Vector2D point);



    public sealed class NelderMeadOptimizer : IDisposable
    {
        private readonly IntPtr _nativeInstance;
        private bool _disposed = false;

        public NelderMeadOptimizer()
        {
            _nativeInstance = NelderMeadNative.CreateNelderMead();
            if (_nativeInstance == IntPtr.Zero)
                throw new InvalidOperationException("Failed to create Nelder-Mead optimizer instance");
        }

        public void SetParameters(NelderMeadParams parameters)
        {
            ThrowIfDisposed();
            NelderMeadNative.SetParameters(_nativeInstance, ref parameters);
        }

        public Vector2D Optimize(Vector2D initialGuess, FunctionPtr function)
        {
            ThrowIfDisposed();

            if (function == null)
                throw new ArgumentNullException(nameof(function));

            int result = NelderMeadNative.Optimize(
                _nativeInstance,
                ref initialGuess,
                function,
                out Vector2D optimizedPoint);

            if (result != 0)
                throw new OptimizationException("Optimization failed");

            return optimizedPoint;
        }

        public IReadOnlyList<SimplexPoint[]> GetHistory()
        {
            ThrowIfDisposed();

            // Первый вызов - получаем количество итераций
            NelderMeadNative.GetHistorySize(_nativeInstance, out int iterationCount);

            if (iterationCount <= 0)
                return Array.Empty<SimplexPoint[]>();

            // Выделяем массив для всех точек (3 точки на каждую итерацию)
            var historyPoints = new SimplexPoint[iterationCount * 3];

            // Второй вызов - получаем сами данные
            NelderMeadNative.GetHistory(_nativeInstance, historyPoints, out int totalPoints);

            // Преобразуем в массив массивов по 3 точки
            var result = new SimplexPoint[iterationCount][];
            for (int i = 0; i < iterationCount; i++)
            {
                result[i] = new SimplexPoint[3];
                Array.Copy(historyPoints, i * 3, result[i], 0, 3);
            }

            return result;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(NelderMeadOptimizer));
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                NelderMeadNative.DestroyNelderMead(_nativeInstance);
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }

        ~NelderMeadOptimizer() => Dispose();
    }

    internal static class NelderMeadNative
    {
        private const string DllName = @"Dll_N_M_2";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr createNelderMead();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void destroyNelderMead(IntPtr instance);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void nelderMeadSetParams(IntPtr instance, ref NelderMeadParams parameters);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int nelderMeadOptimize(
            IntPtr instance,
            ref Vector2D initialGuess,
            [MarshalAs(UnmanagedType.FunctionPtr)] FunctionPtr func,
            out Vector2D result);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void NelderMead_GetHistorySize(IntPtr instance, out int iterationCount);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void NelderMead_GetHistory(
            IntPtr instance,
            [Out] SimplexPoint[] outHistory,
            out int totalPoints);

        // Обёртки с более понятными именами для C#
        public static IntPtr CreateNelderMead() => createNelderMead();
        public static void DestroyNelderMead(IntPtr instance) => destroyNelderMead(instance);
        public static void SetParameters(IntPtr instance, ref NelderMeadParams parameters) =>
            nelderMeadSetParams(instance, ref parameters);
        public static int Optimize(IntPtr instance, ref Vector2D initialGuess, FunctionPtr func, out Vector2D result) =>
            nelderMeadOptimize(instance, ref initialGuess, func, out result);
        public static void GetHistorySize(IntPtr instance, out int iterationCount) =>
            NelderMead_GetHistorySize(instance, out iterationCount);
        public static void GetHistory(IntPtr instance, SimplexPoint[] outHistory, out int totalPoints) =>
            NelderMead_GetHistory(instance, outHistory, out totalPoints);
    }
}