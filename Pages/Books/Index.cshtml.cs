using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Library_Management_System.Pages.Books
{
    public class IndexModel : PageModel
    {
        public List<BookInfo> ListBooks = new List<BookInfo>();
        public void OnGet()
        {
            try {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=library;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))

                {
                    connection.Open();
                    String sql = "SELECT * FROM books";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            while (reader.Read())
                            {
                                BookInfo bookInfo = new BookInfo();
								bookInfo.id = "" + reader.GetInt32(0);
								bookInfo.title = reader.GetString(1);
                                bookInfo.author = reader.GetString(2);
                                bookInfo.isbn = reader.GetString(3);
                                bookInfo.genre = reader.GetString(4);
                                //bookInfo.publishedDate = reader.GetString(5);
                                //bookInfo.created_at = reader.GetString(6);

                                ListBooks .Add(bookInfo);






                            }


                        } 
                    }
                }


            } 
            
            catch (Exception ex) 
            { 
                Console.WriteLine("Exceptin : " + ex.ToString());   
            }
        }
    }

    public class BookInfo {

        public string id;
        public string title;
        public string isbn;
        public string author;
        public string genre;
        //public string publishedDate;
        public string created_at;
    }
}
