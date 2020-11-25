
using System;
using System.Collections.Generic;
using System.Linq;

namespace Softeq.Core.Fourth
{
    public class MouseLine
    {

        private readonly List<char> _line;
        private int _iterations;
        private readonly int _n;
        private readonly int _m;
        public const char BlackMouse = '-';
        public const char WhiteMouse = '+';
        public const char Empty = ' ';
        public event Action<string> OnIteratioinEnd = (lines) => { };

        public static MouseLine Create(int n, int m) => new MouseLine(n, m);

        public MouseLine(int n, int m)
        {
            if (!(n <= 1000 && n >= 1 && m <= 1000 && m >= 1))
            {
                throw new Exception("invalid n and m arguments");
            }
            _n = n;
            _m = m;

            _line = new List<char>(n + m + 1);
            _line.AddRange(Enumerable.Repeat(WhiteMouse, n));
            _line.Add(Empty);
            _line.AddRange(Enumerable.Repeat(BlackMouse, m));

        }

        public override string ToString()
        {
            return new string(_line.ToArray()); ;
        }

        public int FindWithMathFunc()
        {
            //if _n == _m 
            int f(int x) => 1 + 2 * x;

            (int nn, int i) = _n < _m ? (_n, _m - _n) : (_m, _n - _m);
            if (nn == 1 && i == 0) return 3;
            int sum = 0;
            for (int j = 1; j < nn + 1; j++)
            {
                sum += f(j);
            }

            sum += (i * (nn + 1));

            return sum;
            //return Enumerable.Range(1, nn).Select(f).Sum() + (i * (nn + 1));
        }

        public int FindWithPhysicalSort()
        {
            _iterations = 0;

            var line = _line.ToList();
            var reversLines = _line.ToList();
            reversLines.Reverse();
            var indexEmpty = line.IndexOf(Empty);
            line.Swap(indexEmpty - 1, indexEmpty--);
            _iterations++;
            OnIteratioinEnd?.Invoke(new string(line.ToArray()));

            do
            {
                (int indexSource, int indexTarget) = ChoiceAction(ref indexEmpty, line);
                line.Swap(indexSource, indexTarget);
                _iterations++;
                OnIteratioinEnd?.Invoke(new string(line.ToArray()));
            } while (!Compare(line, reversLines));

            return _iterations;

        }

        private bool Compare(List<char> a, List<char> b)
        {
            if (a.Count != b.Count) return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] != b[i]) return false;
            }

            return true;
        }

        private (int, int) ChoiceAction(ref int indexEmpty, List<char> line)
        {
            (int, int) r = (-1, -1);
            int lineLength = line.Count - 1;

            if (indexEmpty == lineLength)
            {
                if (line[indexEmpty - 1] == WhiteMouse)
                {
                    return IdxCh(ref indexEmpty, -1);
                }

                return IdxCh(ref indexEmpty, -2);
            }
            else if (indexEmpty == 0)
            {

                if (line[indexEmpty + 2] == BlackMouse && line[indexEmpty + 1] == WhiteMouse)
                {
                    return IdxCh(ref indexEmpty, 2);

                }
                else if (line[indexEmpty + 2] == WhiteMouse && line[indexEmpty + 1] == BlackMouse)
                {
                    return IdxCh(ref indexEmpty, 1);
                }

                return r;
            }
            else if (line[indexEmpty - 1] == line[indexEmpty + 1])
            {

                if (indexEmpty - 1 == 0)
                {
                    if (line[indexEmpty - 1] == BlackMouse)
                    {
                        return IdxCh(ref indexEmpty, 1);
                    }

                    return IdxCh(ref indexEmpty, 2);
                }
                else if (indexEmpty + 1 == lineLength)
                {
                    if (line[indexEmpty - 1] == BlackMouse)
                    {
                        return IdxCh(ref indexEmpty, -2);

                    }

                    return IdxCh(ref indexEmpty, -1);

                }
                else if (line[indexEmpty - 1] == WhiteMouse)
                {
                    if (line[indexEmpty - 2] == line[indexEmpty + 2])
                    {
                        return IdxCh(ref indexEmpty, 2);

                    }
                    else
                    {
                        if (line[indexEmpty - 2] == WhiteMouse)
                        {
                            return IdxCh(ref indexEmpty, 2);
                        }
                        else
                        {

                            return IdxCh(ref indexEmpty, -1);
                        }
                    }

                }
                else
                {
                    if (line[indexEmpty - 2] == line[indexEmpty + 2])
                    {
                        return IdxCh(ref indexEmpty, -2);
                    }
                    else
                    {
                        if (line[indexEmpty - 2] == WhiteMouse)
                        {
                            return IdxCh(ref indexEmpty, -2);
                        }
                        else
                        {
                            return IdxCh(ref indexEmpty, 1);
                        }

                    }
                }

            }
            else
            {
                if (indexEmpty - 1 == 0)
                {
                    if (line[indexEmpty - 1] == WhiteMouse)
                    {
                        return IdxCh(ref indexEmpty, -1);
                    }
                    else
                    {
                        return IdxCh(ref indexEmpty, 2);
                    }

                }
                else if (indexEmpty + 1 == lineLength)
                {
                    if (line[indexEmpty + 1] == BlackMouse)
                    {
                        return IdxCh(ref indexEmpty, 1);
                    }
                    else
                    {
                        return IdxCh(ref indexEmpty, -2);
                    }
                }
                else if (line[indexEmpty - 1] == WhiteMouse)
                {
                    if (line[indexEmpty - 2] == line[indexEmpty + 2])
                    {
                        if (line[indexEmpty - 2] == WhiteMouse)
                        {
                            return IdxCh(ref indexEmpty, -1);
                        }
                        else
                        {
                            return IdxCh(ref indexEmpty, 1);
                        }
                    }
                }
                else
                {
                    if (line[indexEmpty - 2] == WhiteMouse)
                    {
                        return IdxCh(ref indexEmpty, -2);
                    }
                    else
                    {
                        return IdxCh(ref indexEmpty, 2);
                    }
                }
            }

            return r;
        }

        private (int, int) IdxCh(ref int index, int rate)
        {
            var r = (index + rate, index);
            index += rate;

            return r;
        }

    }

    public static class ListSwapExtension
    {
        public static void Swap<T>(this List<T> self, int source, int target)
        {
            var tmp = self[target];
            self[target] = self[source];
            self[source] = tmp;
        }
    }
}
