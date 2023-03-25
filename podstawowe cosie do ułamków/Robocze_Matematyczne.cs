namespace podstawowe_cosie_do_ułamków
{
	class RoboczeMatematyczne
	{
		static protected bool sgn(int a)
		{
			return a >= 0;
		}

		static public uint absolut(int a)
		{
			if (sgn(a)) return (uint)a;
			return (uint)-a;
		}

		static protected int NWD(int a, int b)
		{
			if (b != 0)  return NWD(b, a%b);
			else return a;
		}
	}

}