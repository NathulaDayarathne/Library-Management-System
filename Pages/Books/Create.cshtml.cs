using System.Data.SqlClient;
using System.Net.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library_Management_System.Pages.Books
{
    public class CreateModel : PageModel
    {
		public BookInfo bookInfo = new BookInfo();
        public string errorMessage = "";
		public string successMessage = "";
		public void OnPost()

        {
            bookInfo.title = Request.Form["title"];
			bookInfo.author = Request.Form["author"];
			bookInfo.genre = Request.Form["genre"];
			bookInfo.isbn = Request.Form["isbn"];

			if (bookInfo.title.Length == 0 || (bookInfo.author.Length == 0) ||
					bookInfo.genre.Length == 0 || bookInfo.isbn.Length == 0) {
				errorMessage = "All the fields are required";
				return;
			}

			//save the data to new book database

			try
			{

				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=library;Integrated Security=True;Encrypt=False";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "INSERT INTO books " +
						"(title, author, genre, isbn) Values " +
						"(@title, @author, @genre, @isbn);";

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
			catch (Exception ex) { 

				errorMessage = ex.Message;
				return;
			}

			bookInfo.title = ""; bookInfo.author = ""; bookInfo.genre = ""; bookInfo.isbn = "";
			successMessage = "New Book Added Successfully!";

			Response.Redirect("/Books/Index");
		}
    }
}
