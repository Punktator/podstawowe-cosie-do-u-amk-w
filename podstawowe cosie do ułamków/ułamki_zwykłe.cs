using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Ulamek32 = podstawowe_cosie_do_ułamków.ułamek_zwykły<int, uint>;

namespace podstawowe_cosie_do_ułamków
{
	using System.CodeDom.Compiler;
	using System.Configuration;
	using System.Diagnostics.CodeAnalysis;
	using System.Numerics;
	using System.Runtime.CompilerServices;

	internal struct ułamek_zwykły/*<TLicznik, TMianownik>
		where TLicznik : INumber<TLicznik> where TMianownik : INumber<TMianownik> */ //: RoboczeMatematyczne
	{
		public int licznik = 0;
		public int mianownik = 1;

		//public ułamek_zwykły(int mianownik) : this()
		//{
		//    mianownik = 1;
		//    this.mianownik = mianownik;
		//    //this.mianownik = 1;
		//}

		public ułamek_zwykły()
		{
			licznik = 0;
			mianownik = 1;
		}

		//public static ułamek_zwykły skruc(ułamek_zwykły a)
		//{
		//    int nwd = RoboczeMatematyczne.NWD(a.licznik, a.mianownik);
		//}

		//public static string convert(ułamek_zwykły a)
		//{
		//    return String.
		//}

		private static void unormuj(ref ułamek_zwykły a, ref ułamek_zwykły b, int mnoznik = 1)
		{
			if (b.mianownik != a.mianownik)
			{
				b.licznik *= a.mianownik;
				a.licznik *= b.mianownik;
				b.mianownik *= a.mianownik;
				a.mianownik = b.mianownik;
			}
			a.licznik *= mnoznik;
			a.mianownik *= mnoznik;
			b.licznik *= mnoznik;
			b.mianownik = a.mianownik;
		}

		private static void unormuj(ref ułamek_zwykły a, ref ułamek_zwykły b, ref ułamek_zwykły c, int mnoznik = 1)
		{
			if (a.mianownik != b.mianownik || b.mianownik != c.mianownik) {
				a.licznik *= b.mianownik * c.mianownik;
				b.licznik *= a.mianownik*c.mianownik;
				c.licznik *= a.mianownik * b.mianownik;
				a.mianownik *= b.mianownik * c.mianownik;
				b.mianownik = a.mianownik;
				c.mianownik = a.mianownik;
			}
			a.licznik *= mnoznik;
			a.mianownik *= mnoznik;
			b.licznik *= mnoznik;
			b.mianownik = a.mianownik;
			c.licznik *= mnoznik;
			c.mianownik = a.mianownik;
		}

		public static int przybliz(ułamek_zwykły a)
		{
			return a.licznik / a.mianownik;
		}

		public static string do_tektu(ułamek_zwykły a)
		{
			return $"{a.licznik}/{a.mianownik}";
		}

		public static ułamek_zwykły operator +(ułamek_zwykły a, ułamek_zwykły b)
		{
			ułamek_zwykły suma;
			unormuj(ref a, ref b);
			suma.licznik = a.licznik + b.licznik;
			suma.mianownik = a.mianownik;
			return suma;
		}

		public static ułamek_zwykły operator + (ułamek_zwykły a, int b)
		{
			ułamek_zwykły suma;
			suma.licznik = a.licznik + b * a.mianownik;
			suma.mianownik = a.mianownik;
			return suma;
		}

		public static ułamek_zwykły operator -(ułamek_zwykły a, ułamek_zwykły b)
		{
			ułamek_zwykły suma;
			unormuj(ref a, ref b);
			suma.licznik = a.licznik - b.licznik;
			suma.mianownik = a.mianownik;
			return suma;
		}

		public static ułamek_zwykły operator -(ułamek_zwykły a, int b)
		{
			a.licznik -= b * a.mianownik;
			return a;
		}

		public static ułamek_zwykły operator *(ułamek_zwykły a, ułamek_zwykły b)
		{
			ułamek_zwykły iloczyn;
			
			if (RoboczeMatematyczne.absolut(a.licznik) == RoboczeMatematyczne.absolut(b.mianownik))
			{
				if (a.licznik >= 0 == b.mianownik >= 0) a.licznik = 1;
				else a.licznik = -1;
				b.mianownik = 1;
			}
			if (RoboczeMatematyczne.absolut(a.mianownik) == RoboczeMatematyczne.absolut(b.licznik))
			{
				a.mianownik = 1;
				if (a.mianownik >= 0 ) b.licznik = 1;
			}

			iloczyn.licznik = a.licznik * b.licznik;
			iloczyn.mianownik = a.mianownik * b.mianownik;
			return iloczyn;
		}

		public static ułamek_zwykły operator /(ułamek_zwykły a, ułamek_zwykły b)
		{
			if (RoboczeMatematyczne.absolut(a.licznik) == RoboczeMatematyczne.absolut(b.licznik))
			{
				if (a.licznik >= 0 == b.licznik >= 0) a.licznik = 1;
				else a.licznik = -1;
				b.licznik = 1;
			}
			if (/*RoboczeMatematyczne.absolut*/(a.mianownik) == /*RoboczeMatematyczne.absolut*/(b.mianownik))
			{
				a.mianownik = 1;
				b.mianownik = 1;
			}

			ułamek_zwykły iloraz;
			iloraz.licznik = a.licznik * b.mianownik;
			iloraz.mianownik = a.mianownik * b.licznik;
			return iloraz;
		}

		public static int operator %(ułamek_zwykły a, ułamek_zwykły b)
		{
			return (a / b).licznik % (a / b).mianownik;
		}

		public static ułamek_zwykły operator ++(ułamek_zwykły a)
		{
			a += 1;
			return a;
		}

		public static ułamek_zwykły operator --(ułamek_zwykły a)
		{
			a -= 1;
			return a;
		}

		public static bool operator !=(ułamek_zwykły a, ułamek_zwykły b)
		{
			unormuj(ref a, ref b);
			return a.licznik != b.licznik;
		}

		public static bool operator ==(ułamek_zwykły a, ułamek_zwykły b)
		{
			unormuj(ref a, ref b);
			return a.licznik == b.licznik;
		}

	   public static bool operator <(ułamek_zwykły a, ułamek_zwykły b)
		{
			unormuj(ref a, ref b);
			return a.licznik < b.licznik;
		}

		public static bool operator <=(ułamek_zwykły a, ułamek_zwykły b)
		{
			unormuj(ref a, ref b);
			return a.licznik <= b.licznik;
		}

		public static bool operator >(ułamek_zwykły a, ułamek_zwykły b)
		{
			unormuj(ref a, ref b);
			return a.licznik > b.licznik;
		}

		public static bool operator >= (ułamek_zwykły a, ułamek_zwykły b)
		{
			unormuj(ref a, ref b);
			return a.licznik >= b.licznik;
		}

		public static bool equals(ułamek_zwykły a, ułamek_zwykły b)
		{
			return a.licznik == b.licznik && a.mianownik == b.mianownik;
		}

		public static void testgoto()
		{
			goto test;
			for (; ; ) { }
		test: 
			return;
		}
	}

	//interface ułamek_zwykły
	//{
	//    public
	//}
}
