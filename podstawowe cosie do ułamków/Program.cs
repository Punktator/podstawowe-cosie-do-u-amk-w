namespace podstawowe_cosie_do_ułamków
{
	using System;
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Console.WriteLine(int.MaxValue);
			//ułamek_zwykły a = { 0, 1 };
			ułamek_zwykły a;
			a.mianownik = 1;
			a.licznik = 2;
			//a = { 0, 1};
   
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
		   // ApplicationConfiguration.Initialize();
			//Application.Run(new Form1());
		}
	}
}