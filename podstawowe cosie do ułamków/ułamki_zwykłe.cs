using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Ulamek32 = podstawowe_cosie_do_ułamków.ulamekzwykly<int, uint>;

namespace podstawowe_cosie_do_ułamków
{
	using System.CodeDom.Compiler;
	using System.Configuration;
	using System.Diagnostics.CodeAnalysis;
	using System.Numerics;
	using System.Runtime.CompilerServices;

	internal struct ulamekzwykly/*<TLicznik, TMianownik>
		where TLicznik : INumber<TLicznik> where TMianownik : INumber<TMianownik> */ //: RoboczeMatematyczne
	{
		public int licznik = 0;
		public int mianownik = 1;

		//public ulamekzwykly(int mianownik) : this()
		//{
		//    mianownik = 1;
		//    this.mianownik = mianownik;
		//    //this.mianownik = 1;
		//}

		public ulamekzwykly()
		{
			licznik = 0;
			mianownik = 1;
		}

		//public static ulamekzwykly skruc(ulamekzwykly a)
		//{
		//    int nwd = RoboczeMatematyczne.NWD(a.licznik, a.mianownik);
		//}

		//public static string convert(ulamekzwykly a)
		//{
		//    return String.
		//}

		private static void unormuj(ref ulamekzwykly a, ref ulamekzwykly b, int mnoznik = 1)
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

		private static void unormuj(ref ulamekzwykly a, ref ulamekzwykly b, ref ulamekzwykly c, int mnoznik = 1)
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

		public static int przybliz(ulamekzwykly a)
		{
			return a.licznik / a.mianownik;
		}

		public static string ulamek_zwykly_do_tektu(ulamekzwykly a)
		{
			return $"{a.licznik}/{a.mianownik}";
		}

		public static ulamekzwykly operator +(ulamekzwykly a, ulamekzwykly b)
		{
			ulamekzwykly suma;
			unormuj(ref a, ref b);
			suma.licznik = a.licznik + b.licznik;
			suma.mianownik = a.mianownik;
			return suma;
		}

		//public static ulamekzwykly operator + <TLiczba>(ulamekzwykly a)

		public static ulamekzwykly operator -(ulamekzwykly a, ulamekzwykly b)
		{
			ulamekzwykly suma;
			unormuj(ref a, ref b);
			suma.licznik = a.licznik - b.licznik;
			suma.mianownik = a.mianownik;
			return suma;
		}

		public static ulamekzwykly operator *(ulamekzwykly a, ulamekzwykly b)
		{
			ulamekzwykly iloczyn;
			
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

		public static ulamekzwykly operator /(ulamekzwykly a, ulamekzwykly b)
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

			ulamekzwykly iloraz;
			iloraz.licznik = a.licznik * b.mianownik;
			iloraz.mianownik = a.mianownik * b.licznik;
			return iloraz;
		}

		public static int operator %(ulamekzwykly a, ulamekzwykly b)
		{
			return (a / b).licznik % (a / b).mianownik;
		}

		public static ułamek_zwykły operator ++(ułamek_zwykły a)
		{
			unormuj(ref a, ref b);
			return a.licznik != b.licznik;
		}

		public static bool operator ==(ulamekzwykly a, ulamekzwykly b)
		{
			unormuj(ref a, ref b);
			return a.licznik == b.licznik;
		}

	   public static bool operator <(ulamekzwykly a, ulamekzwykly b)
		{
			unormuj(ref a, ref b);
			return a.licznik < b.licznik;
		}

		public static bool operator <=(ulamekzwykly a, ulamekzwykly b)
		{
			unormuj(ref a, ref b);
			return a.licznik <= b.licznik;
		}

		public static bool operator >(ulamekzwykly a, ulamekzwykly b)
		{
			unormuj(ref a, ref b);
			return a.licznik > b.licznik;
		}

		public static bool operator >= (ulamekzwykly a, ulamekzwykly b)
		{
			unormuj(ref a, ref b);
			return a.licznik >= b.licznik;
		}

		public static bool equals(ulamekzwykly a, ulamekzwykly b)
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

	//interface ulamekzwykly
	//{
	//    public
	//}
}
