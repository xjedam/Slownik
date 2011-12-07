using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Slownik
{
    class Program
    {
		
		
        static void Main(string[] args)
        {
            DatabaseManager dbm = new DatabaseManager();
			Console.WriteLine("Slownik ver 0.0.00001 by Ryszard Madejski.");
			
			char input = 'a';
			while(input != '0')
			{
				Console.WriteLine("\nDostępne operacje:");
				Console.WriteLine("1 - wypisz listę definicji ze słownika");
				Console.WriteLine("2 - dodaj wpis");
				Console.WriteLine("3 - usuń wpis");
				Console.WriteLine("4 - edytuj wpis");
				Console.WriteLine("5 - przetłumacz z języka polskiego na agnielski");
				Console.WriteLine("6 - przetłumacz z języka angielskiego na polski");
				Console.WriteLine("0 - wyjście");
				Console.Write("Wybór: ");
				
				input = (char) System.Console.Read();
				switch(input)
				{
					case '1':
						List<Wpis> listaWpisow = dbm.zwrocListeWpisow();
						wypiszListeWpisow(listaWpisow);
						break;
					case '2':
						dbm.zapiszWpis(zwrocWpisZWejscia());
						break;
					case '3':
						dbm.usunWpis(usuwanieWpisu());
						break;
					case '4':
						Console.Write("\nPodaj id wpisu do edycji: ");
						long id = long.Parse(Console.ReadLine());
						Wpis w = edytujWpis(dbm.zwrocWpis(id));
						dbm.zapiszWpis(w);
						break;
					case '5':
						Console.Write("\nPodaj słowo po polsku: ");
						String pl = Console.ReadLine();
						wypiszTlumaczenia(dbm.zwrocTlumaczeniaAngielskie(pl.ToLower()));
						break;
					case '6':
						Console.Write("\nPodaj słowo po angielsku: ");
						String en = Console.ReadLine();
						wypiszTlumaczenia(dbm.zwrocTlumaczeniaPolskie(en.ToLower()));
						break;
				}
			}
			
			//dbm.zapiszWpis(new Wpis("tabela", "tableaaaa"));
			//dbm.usunWpis(3);
			//Wpis wpis = dbm.zwrocWpis(6);
			//wpis.angielskie = "table";
			//dbm.zapiszWpis(wpis);
			//List<String> l = dbm.zwrocTlumaczeniaAngielskie("rower");
        }
		
		private static void wypiszListeWpisow(List<Wpis> wpisy)
		{
			Console.WriteLine("\nLista wpisów:\n");	
			Console.WriteLine("{0, -5} | {1, -20} | {2, -20}", "ID", "Polskie", "Angielskie");
			Console.WriteLine("-----------------------------------------------");
			foreach(Wpis w in wpisy)
			{
				Console.WriteLine("{0, -5} | {1, -20} | {2, -20}", w.id, w.polskie, w.angielskie);
			}
			Console.WriteLine("");
		}
		
		private static Wpis zwrocWpisZWejscia()
		{
			Console.Write("\nPodaj słowo po polsku: ");
			String pl = Console.ReadLine();
			Console.Write("Podaj znaczenie po angielsku: ");
			String en = Console.ReadLine();
			return new Wpis(pl.ToLower(), en.ToLower());
		}
		
		private static long usuwanieWpisu()
		{
			Console.Write("\nPodaj id wpisu do usunięcia: ");
			String idStr = Console.ReadLine();
			return long.Parse(idStr);
		}
		
		private static Wpis edytujWpis(Wpis wpis)
		{
			Console.WriteLine("Edycja wpisu PL: {0} EN: {1} ", wpis.polskie, wpis.angielskie);
			Console.Write("\nPodaj poprawione słowo po polsku(puste - brak zmiany): ");
			String pl = Console.ReadLine();
			Console.Write("\nPodaj poprawione słowo po angielsku(puste - brak zmiany): ");
			String en = Console.ReadLine();
			if(!pl.Equals(""))
				wpis.polskie = pl.ToLower();
			if(!en.Equals(""))
				wpis.angielskie = en.ToLower();
			
			return wpis;
		}
		
		private static void wypiszTlumaczenia(List<String> tlumaczenia)
		{
			foreach(String s in tlumaczenia)
			{
				Console.Write("{0}  ", s);
			}
			Console.WriteLine("");
		}

    }
}
