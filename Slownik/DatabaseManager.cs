using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Slownik
{
    class DatabaseManager
    {
        private SqlConnection connection = new SqlConnection("Data Source=eos.inf.ug.edu.pl;Initial Catalog=rmadejski;User Id=rmadejski;Password=186467;");

        public List<Wpis> zwrocListeWpisow()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM slownik", connection);
            List<Wpis> lista = new List<Wpis>();
			try
			{
            	connection.Open();
			}
			catch(Exception e)
			{
				Console.WriteLine("Błąd: " + e.ToString());
				connection.Close();
				return null;
			}
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //Console.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
				Wpis w = new Wpis(reader.GetString(1), reader.GetString(2));
				w.id = reader.GetInt32(0);
                lista.Add(w);
            }
            connection.Close();
            return lista;
        }
		
		public Wpis zwrocWpis(long id)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM slownik WHERE id = " + id, connection);
            try
			{
            	connection.Open();
			}
			catch(Exception e)
			{
				Console.WriteLine("Błąd: " + e.ToString());
				connection.Close();
				return null;
			}
			Wpis wpis = null;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
				//Console.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
				wpis = new Wpis(reader.GetString(1), reader.GetString(2));
				wpis.id = reader.GetInt32(0);
            }
            connection.Close();
            return wpis;
        }
		
		public List<String> zwrocTlumaczeniaAngielskie(String polskieSlowo)
		{
			SqlCommand cmd = new SqlCommand("SELECT angielskie FROM slownik WHERE polskie = '" + polskieSlowo + "'", connection);
			connection.Open();
			List<String> lista = new List<String>();
			SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //Console.WriteLine("{0}", reader.GetString(0));
                lista.Add(reader.GetString(0));
            }
			connection.Close();
            return lista;
		}
		
		public List<String> zwrocTlumaczeniaPolskie(String angielskieSlowo)
		{
			SqlCommand cmd = new SqlCommand("SELECT polskie FROM slownik WHERE angielskie = '" + angielskieSlowo + "'", connection);
        	connection.Open();
			List<String> lista = new List<String>();
			SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //Console.WriteLine("{0}", reader.GetString(0));
                lista.Add(reader.GetString(0));
            }
			connection.Close();
            return lista;
		}
		
		public void zapiszWpis(Wpis wpis)
        {
	        try
        	{
			SqlCommand cmd = null;
			if(wpis.id == -1)
            	cmd = new SqlCommand("insert into slownik(polskie, angielskie) values('"+ wpis.polskie +"', '"+ wpis.angielskie +"' )", connection);
			else
				cmd = new SqlCommand("update slownik set polskie = '"+ wpis.polskie +"', angielskie = '"+ wpis.angielskie +"' where id = " + wpis.id, connection);

            	connection.Open();
				cmd.ExecuteScalar();
            	connection.Close();

	            
			}
			catch(SqlException e)
			{
				Console.WriteLine("Błąd: " + e.ToString());
				connection.Close();
			}
        }
		
		public void usunWpis(long id)
        {
	        try
        	{
				SqlCommand cmd = new SqlCommand("delete from slownik where id = " + id, connection);
            	connection.Open();
				cmd.ExecuteNonQuery();
            	connection.Close();      
			}
			catch(SqlException e)
			{
				Console.WriteLine("Błąd: " + e.ToString());
				connection.Close();
			}
        }
    }
}
/*
drop table slownik;
go

create table slownik( ID int IDENTITY(1,1), polskie varchar(40), angielskie varchar(40));
go

begin
  insert into slownik values( 1, 'kot', 'cat' );
  insert into slownik values( 2, 'jabłko', 'apple' );
  insert into slownik values( 3, 'stół', 'table' );
  insert into slownik values( 4, 'tabela', 'table' );
  insert into slownik values( 5, 'rower', 'bike' );
  insert into slownik values( 6, 'rower', 'bicycle' );
end;
go

select * from slownik;
go 
 
 */
