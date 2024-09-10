using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library_Management_System.Pages.Books
{
    public class EditModel : PageModel
    {
        public BookInfo bookInfo = new BookInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];


            try
            {
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=library;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString)) { 
                    connection.Open();
                    String sql = "SELECT * FROM books WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection)){ 
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            if (reader.Read()) {
                                bookInfo.id = "" + reader.GetInt32(0);
								bookInfo.title = "" + reader.GetString(1);
								bookInfo.author = "" + reader.GetString(2);
								bookInfo.isbn = "" + reader.GetString(3);
								bookInfo.genre = "" + reader.GetString(4);

							}
                        }
                    }

                
                }

				}

            catch (Exception ex) { 

                errorMessage = ex.Message;

            
            }
        }

        public void OnPost() {

            bookInfo.id = Request.Form["id"];
			bookInfo.title = Request.Form["title"];
			bookInfo.author = Request.Form["author"];
			bookInfo.isbn = Request.Form["isbn"];
			bookInfo.genre = Request.Form["genre"];

            if (bookInfo.id.Length == 0 || bookInfo.title.Length == 0 || bookInfo.author.Length == 0 ||
				bookInfo.isbn.Length == 0 || bookInfo.genre.Length == 0 )

            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=library;Integrated Security=True;Encrypt=False";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "UPDATE books " +
						"SET title=@title, author=@author, genre=@genre, isbn=@isbn)" +  
						"WHERE id=@id";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@title", bookInfo.title);
						command.Parameters.AddWithValue("@author", bookInfo.author);
						command.Parameters.AddWithValue("@genre", bookInfo.genre);
						command.Parameters.AddWithValue("@isbn", bookInfo.isbn);

						command.ExecuteNonQuery();
					}
				}
			}

            catch (Exception ex) 
            
            { 
                errorMessage=ex.Message;
                return;
            
            }

            Response.Redirect("/Books/Index");
		}    
    }
}
