namespace NumberTheory;

public static class SieveOfEratosthenes
{
    /// <summary>
    /// Generates a sequence of prime numbers up to the specified limit using a sequential approach.
    /// </summary>
    /// <param name="n">The upper limit for generating prime numbers.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing prime numbers up to the specified limit.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the input <paramref name="n"/> is less than or equal to 0.</exception>
    public static IEnumerable<int> GetPrimeNumbersSequentialAlgorithm(int n)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(n, 0);

        bool[] isPrime = new bool[n + 1];

        for (int i = 2; i <= n; i++)
        {
            isPrime[i] = true;
        }

        for (int i = 2; i * i <= n; i++)
        {
            if (isPrime[i])
            {
                for (int j = i * i; j <= n; j += i)
                {
                    isPrime[j] = false;
                }
            }
        }

        List<int> primes = new();

        for (int i = 2; i <= n; i++)
        {
            if (isPrime[i])
            {
                primes.Add(i);
            }
        }

        return primes;
    }

    /// <summary>
    /// Generates a sequence of prime numbers up to the specified limit using a modified sequential approach.
    /// </summary>
    /// <param name="n">The upper limit for generating prime numbers.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing prime numbers up to the specified limit.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the input <paramref name="n"/> is less than or equal to 0.</exception>
    public static IEnumerable<int> GetPrimeNumbersModifiedSequentialAlgorithm(int n)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(n, 0);

        List<int> basePrimes = new();

        // stage 1: get base prime numbers
        int i = 0;
        for (i = 2; i * i <= n; i++)
        {
            bool isComposite = false;

            foreach (int prime in basePrimes)
            {
                if (prime * prime > i)
                {
                    break;
                }

                if (i % prime == 0)
                {
                    isComposite = true;
                    break;
                }
            }

            if (!isComposite)
            {
                basePrimes.Add(i);
            }
        }

        List<int> newPrimes = new();

        // stage 2: find the new primes
        for (int j = i; j <= n; j++)
        {
            bool isComposite = false;

            foreach (int prime in basePrimes)
            {
                if (prime * prime > j)
                {
                    break;
                }

                if (j % prime == 0)
                {
                    isComposite = true;
                    break;
                }
            }

            if (!isComposite)
            {
                newPrimes.Add(j);
            }
        }

        return basePrimes.Concat(newPrimes);
    }

    /// <summary>
    /// Generates a sequence of prime numbers up to the specified limit using a concurrent approach by data decomposition.
    /// </summary>
    /// <param name="n">The upper limit for generating prime numbers.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing prime numbers up to the specified limit.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the input <paramref name="n"/> is less than or equal to 0.</exception>
    public static IEnumerable<int> GetPrimeNumbersConcurrentDataDecomposition(int n)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(n, 0);

        List<int> basePrimes = new();

        // stage 1: get base prime numbers
        int i = 0;
        for (i = 2; i * i <= n; i++)
        {
            bool isComposite = false;

            foreach (int prime in basePrimes)
            {
                if (prime * prime > i)
                {
                    break;
                }

                if (i % prime == 0)
                {
                    isComposite = true;
                    break;
                }
            }

            if (!isComposite)
            {
                basePrimes.Add(i);
            }
        }

        List<int> newPrimes = new();

        // split the indices into chunks
        int threadCount = Environment.ProcessorCount;
        int rangeToProcess = n - i + 1;
        int chunkLength = rangeToProcess / threadCount;
        int remainder = rangeToProcess % threadCount;

        List<int> chunks = new();

        for (int j = 0; j < threadCount; j++)
        {
            chunks.Add(chunkLength);
        }

        for (int j = 0; j < remainder; j++)
        {
            chunks[j]++;
        }

        List<(int Start, int End)> ranges = new();

        int currentStart = i;

        foreach (int size in chunks)
        {
            int start = currentStart;
            int end = currentStart + size - 1;

            ranges.Add((start, end));

            currentStart = end + 1;
        }

        List<Thread> threads = new();
        object locker = new();

        foreach (var range in ranges)
        {
            var localRange = range;

            Thread t = new Thread(() =>
            {
                for (int j = localRange.Start; j <= localRange.End; j++)
                {
                    bool isComposite = false;

                    foreach (int prime in basePrimes)
                    {
                        if (prime * prime > j)
                        {
                            break;
                        }

                        if (j % prime == 0)
                        {
                            isComposite = true;
                            break;
                        }
                    }

                    if (!isComposite)
                    {
                        lock (locker)
                        {
                            newPrimes.Add(j);
                        }
                    }
                }
            });

            t.Start();

            threads.Add(t);
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        return basePrimes.Concat(newPrimes);
    }

    /// <summary>
    /// Generates a sequence of prime numbers up to the specified limit using a concurrent approach by "basic" primes decomposition.
    /// </summary>
    /// <param name="n">The upper limit for generating prime numbers.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing prime numbers up to the specified limit.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the input <paramref name="n"/> is less than or equal to 0.</exception>
    public static IEnumerable<int> GetPrimeNumbersConcurrentBasicPrimesDecomposition(int n)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(n, 0);

        List<int> basePrimes = new();

        // stage 1: get base prime numbers
        int i = 0;
        for (i = 2; i * i <= n; i++)
        {
            bool isComposite = false;

            foreach (int prime in basePrimes)
            {
                if (prime * prime > i)
                {
                    break;
                }

                if (i % prime == 0)
                {
                    isComposite = true;
                    break;
                }
            }

            if (!isComposite)
            {
                basePrimes.Add(i);
            }
        }

        bool[] isPrime = new bool[n + 1];
        for (int j = i; j <= n; j++)
        {
            isPrime[j] = true;
        }

        (int Start, int End) range = (i, n);

        int threadCount = Environment.ProcessorCount;
        int chunkSize = (basePrimes.Count + threadCount - 1) / threadCount;

        List<Thread> threads = new();

        for (int t = 0; t < threadCount; t++)
        {
            int startIdx = t * chunkSize;
            int endIdx = Math.Min(startIdx + chunkSize, basePrimes.Count);

            if (startIdx >= endIdx)
            {
                break;
            }

            Thread thread = new Thread(() =>
            {
                for (int pIndex = startIdx; pIndex < endIdx; pIndex++)
                {
                    int prime = basePrimes[pIndex];

                    int start = Math.Max(
                        prime * prime,
                        ((range.Start + prime - 1) / prime) * prime);

                    for (int j = start; j <= range.End; j += prime)
                    {
                        isPrime[j] = false;
                    }
                }
            });

            thread.Start();

            threads.Add(thread);
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        List<int> newPrimes = new();

        for (int j = range.Start; j <= range.End; j++)
        {
            if (isPrime[j])
            {
                newPrimes.Add(j);
            }
        }

        return basePrimes.Concat(newPrimes);
    }

    /// <summary>
    /// Generates a sequence of prime numbers up to the specified limit using thread pool and signaling construct.
    /// </summary>
    /// <param name="n">The upper limit for generating prime numbers.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing prime numbers up to the specified limit.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the input <paramref name="n"/> is less than or equal to 0.</exception>
    public static IEnumerable<int> GetPrimeNumbersConcurrentWithThreadPool(int n)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(n, 0);

        List<int> basePrimes = new();

        // stage 1: get base prime numbers
        int i = 0;
        for (i = 2; i * i <= n; i++)
        {
            bool isComposite = false;

            foreach (int prime in basePrimes)
            {
                if (prime * prime > i)
                {
                    break;
                }

                if (i % prime == 0)
                {
                    isComposite = true;
                    break;
                }
            }

            if (!isComposite)
            {
                basePrimes.Add(i);
            }
        }

        bool[] isPrime = new bool[n + 1];
        for (int j = i; j <= n; j++)
        {
            isPrime[j] = true;
        }

        (int Start, int End) range = (i, n);

        using CountdownEvent countdown = new(basePrimes.Count);

        foreach (var prime in basePrimes)
        {
            int localPrime = prime;

            ThreadPool.QueueUserWorkItem(_ =>
            {
                int start = Math.Max(
                    localPrime * localPrime,
                    ((range.Start + localPrime - 1) / localPrime) * localPrime);

                for (int j = start; j <= range.End; j += localPrime)
                {
                    isPrime[j] = false;
                }

                countdown.Signal();
            });
        }

        countdown.Wait();

        List<int> newPrimes = new();

        for (int j = range.Start; j <= range.End; j++)
        {
            if (isPrime[j])
            {
                newPrimes.Add(j);
            }
        }

        return basePrimes.Concat(newPrimes);
    }
}
