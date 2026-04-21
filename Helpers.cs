using System.Numerics;
using System.Text;

namespace Bombardino
{
    internal static class Helpers
    {
    }

    public static class StringHelpers
    {
        public static void PrintOnScreen(this string message, int x = 0, int y = 0)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(message.PadRight(Console.WindowWidth - x));
        }

        public static void PrintBellowCanvas(this string message, int y = 0)
        {
            PrintOnScreen(message, 0, y + GameManager.Instance.BellowCanvas);
        }

        public static readonly Dictionary<char, string[]> Char2String = new Dictionary<char, string[]>
        {
            ['A'] = new[]
        {
            "   AAAA   ",
            "  AA  AA  ",
            " AAAAAAAA ",
            " AA    AA ",
            " AA    AA "
        },
            ['B'] = new[]
        {
            " BBBBBB   ",
            " BB    BB ",
            " BBBBBB   ",
            " BB    BB ",
            " BBBBBB   "
        },
            ['C'] = new[]
        {
            "   CCCCC  ",
            "  CC      ",
            " CC       ",
            "  CC      ",
            "   CCCCC  "
        },
            ['D'] = new[]
        {
            " DDDDDD   ",
            " DD   DD  ",
            " DD    DD ",
            " DD   DD  ",
            " DDDDDD   "
        },
            ['E'] = new[]
        {
            " EEEEEEE  ",
            " EE       ",
            " EEEEE    ",
            " EE       ",
            " EEEEEEE  "
        },
            ['F'] = new[]
        {
            " FFFFFFFF ",
            " FF       ",
            " FFFFFF   ",
            " FF       ",
            " FF       "
        },
            ['G'] = new[]
        {
            "  GGGGGG  ",
            " GG       ",
            " GG  GGG  ",
            " GG    GG ",
            "   GGGGG  "
        },
            ['H'] = new[]
        {
            " HH    HH ",
            " HH    HH ",
            " HHHHHHHH ",
            " HH    HH ",
            " HH    HH "
        },
            ['I'] = new[]
        {
            " IIIIIIII ",
            "    II    ",
            "    II    ",
            "    II    ",
            " IIIIIIII "
        },
            ['J'] = new[]
        {
            "    JJJJ  ",
            "      JJ  ",
            "      JJ  ",
            "  JJ  JJ  ",
            "   JJJJ   "
        },
            ['K'] = new[]
        {
            " KK   KKK ",
            " KK  KK   ",
            " KKKK     ",
            " KK  KK   ",
            " KK   KKK "
        },
            ['L'] = new[]
        {
            "  LL      ",
            "  LL      ",
            "  LL      ",
            "  LL      ",
            "  LLLLLLL "
        },
            ['M'] = new[]
        {
            " MM    MM ",
            " MMM  MMM ",
            " M  MM  M ",
            " M      M ",
            " M      M "
        },
            ['N'] = new[]
        {
            " NN    NN ",
            " NNN   NN ",
            " NN NN NN ",
            " NN  NNNN ",
            " NN    NN "
        },
            ['O'] = new[]
        {
            "   OOOO   ",
            " OO    OO ",
            " OO    OO ",
            " OO    OO ",
            "   OOOO   "
        },
            ['P'] = new[]
        {
            " PPPPPPP  ",
            " P     PP ",
            " PPPPPPP  ",
            " PP       ",
            " PP       "
        },
            ['Q'] = new[]
        {
            "  QQQQQQ  ",
            " QQ    QQ ",
            " QQ    QQ ",
            " QQ  Q QQ ",
            "   QQQ QQ "
        },
            ['R'] = new[]
        {
            " RRRRRRR  ",
            " R     RR ",
            " RRRRRRR  ",
            " RR   RR  ",
            " RR    RR "
        },
            ['S'] = new[]
        {
            "  SSSSSSS ",
            " SSS      ",
            "   SSS    ",
            "      SSS ",
            " SSSSSSS  "
        },
            ['T'] = new[]
        {
            " TTTTTTTT ",
            "    TT    ",
            "    TT    ",
            "    TT    ",
            "    TT    "
        },
            ['U'] = new[]
        {
            " UU    UU ",
            " UU    UU ",
            " UU    UU ",
            " UU    UU ",
            "  UUUUUU  "
        },
            ['V'] = new[]
        {
            " VV    VV ",
            " VV    VV ",
            "  VV  VV  ",
            "   VVVV   ",
            "    VV    "
        },
            ['W'] = new[]
        {
            " WW    WW ",
            " WW    WW ",
            " WW WW WW ",
            " WW WW WW ",
            " W      W "
        },
            ['X'] = new[]
        {
            " XX    XX ",
            "  XX  XX  ",
            "    XX    ",
            "  XX  XX  ",
            " XX    XX "
        },
            ['Y'] = new[]
        {
            " YY    YY ",
            "  YY  YY  ",
            "    YY    ",
            "    YY    ",
            "    YY    "
        },
            ['Z'] = new[]
        {
            " ZZZZZZZZ ",
            "     ZZ   ",
            "    ZZ    ",
            "  ZZ      ",
            " ZZZZZZZZ "
        }
        };

        public static readonly char[,] PlayerSprite = GetSpriteFromChar('P');
        public static readonly char[,] InvornablePlayerSprite = GetSpriteFromChar('I');

        public static char[,] GetSpriteFromChar(char c)
        {
            return StringToCharArray(Char2String[c]);
        }

        public static void SanityCheck()
        {
            foreach (char c in Char2String.Keys)
            {
                var sprite = Char2String[c];

                if (sprite.GetLength(0) != 5)
                {
                    throw new ArgumentException(sprite[1]);
                }

                for (int i = 0; i < sprite.GetLength(0); i++)
                {
                    if (sprite[i].Length != 10) 
                        throw new ArgumentException($"{c}:{i}, {sprite[i]}");
                }
            }
        }

        public static char[,] StringToCharArray(string[] incoming)
        {

            char[,] sprite = new char[incoming.Length, incoming[0].Length];
            for (int i = 0; i < incoming.Length; i++)
            {
                for (int j = 0; j < incoming[i].Length; j++)
                {
                    sprite[i, j] = incoming[i][j];
                }
            }
            return sprite;
        }
    }

    #region Maths Helpers
    public struct Vec2<T> where T : INumber<T>
    {
        public T x { get; set; }
        public T y { get; set; }

        public Vec2(T x, T y) { this.x = x; this.y = y; }

        // Swizzling and whatnot
        public readonly Vec2<T> xx => new(x, x);
        public readonly Vec2<T> xy => new(x, y);
        public readonly Vec2<T> yy => new(y, y);

        public static Vec2<T> Up => new(T.Zero, -T.One);
        public static Vec2<T> Down => new(T.Zero, T.One);
        public static Vec2<T> Left => new(-T.One, T.Zero);
        public static Vec2<T> Right => new(T.One, T.Zero);
        public static Vec2<T> Zero => new(T.Zero, T.Zero);
        public static Vec2<T> One => new(T.One, T.One);

        // Operators
        public static bool operator ==(Vec2<T> lhs, Vec2<T> rhs) => lhs.x == rhs.x && lhs.y == rhs.y;
        public static bool operator !=(Vec2<T> lhs, Vec2<T> rhs) => !(lhs == rhs);
        public static Vec2<T> operator +(Vec2<T> lhs, Vec2<T> rhs) => new(lhs.x + rhs.x, lhs.y + rhs.y);
        public static Vec2<T> operator -(Vec2<T> v) => new Vec2<T>(-v.x, -v.y);
        public static Vec2<T> operator -(Vec2<T> lhs, Vec2<T> rhs) => new(lhs.x - rhs.x, lhs.y - rhs.y);
        public static Vec2<T> operator *(Vec2<T> vec, T scalar) => new(vec.x * scalar, vec.y * scalar);
        public static Vec2<T> operator /(Vec2<T> vec, T scalar) => new(vec.x / scalar, vec.y / scalar);

        public static T DistanceSqr(Vec2<T> lhs, Vec2<T> rhs)
        {
            T dx = lhs.x - rhs.x;
            T dy = lhs.y - rhs.y;
            return dx * dx + dy * dy;
        }

        public static Vec2<T> Abs(Vec2<T> v)
        {
            if (v.x < T.Zero) v.x *= -T.One;
            if (v.y < T.Zero) v.y *= -T.One;
            return v;
        }

        public static T Dot(Vec2<T> lhs, Vec2<T> rhs) => lhs.x * rhs.x + lhs.y * rhs.y;

        public override readonly bool Equals(object? obj)
        {
            if (obj is null || obj is not Vec2<T>) return false;
            return this == (Vec2<T>)obj;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        public override string ToString()
        {
            return $"float2({x}, {y})";
        }
    }
    #endregion
}
