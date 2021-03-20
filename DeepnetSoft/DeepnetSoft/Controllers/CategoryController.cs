using DeepnetSoft.Model;
using DeepnetSoft.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DeepnetSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public CategoryController(IConfiguration config)
        {
            this.configuration = config;
        }

        [HttpGet("{id}")]
        public CategoryProductViewModel Get(string id)
        {
            CategoryProductViewModel categoryViewModel = new CategoryProductViewModel();
            categoryViewModel.Categories = GetSubCategoriesByCategoryID(id);
            categoryViewModel.Products = GetProductsByCategoryID(id);

            return categoryViewModel;
        }
        private List<Category> GetSubCategoriesByCategoryID(string id)
        {
            string querry = @"SELECT * FROM dbo.category WHERE parent_category_id = " + id +@"";
            DataTable dataTable = new DataTable();
            string SqlDataSource = configuration.GetConnectionString("ProductAppCon");
            SqlDataReader reader;
            using(SqlConnection sqlConnection=new SqlConnection(SqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand command= new SqlCommand(querry, sqlConnection))
                {
                    reader = command.ExecuteReader();
                    dataTable.Load(reader);
                    reader.Close();
                    sqlConnection.Close(); 

                }

            }
            List < Category> categories = (from DataRow row in dataTable.Rows

                                            select new Category
                                            {
                                                CategoryID = (int)row["category_id"],
                                                CategoryName = row["category_name"].ToString()

                                            }).ToList();
            return  categories;
        }
        private List<Product> GetProductsByCategoryID(string id)
        {
            string querry = @"SELECT * FROM dbo.product WHERE category_id = " + id + @"";
            DataTable dataTable = new DataTable();
            string SqlDataSource = configuration.GetConnectionString("ProductAppCon");
            SqlDataReader reader;
            using (SqlConnection sqlConnection = new SqlConnection(SqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(querry, sqlConnection))
                {
                    reader = command.ExecuteReader();
                    dataTable.Load(reader);
                    reader.Close();
                    sqlConnection.Close();

                }

            }
            List<Product> products = (from DataRow row in dataTable.Rows

                                         select new Product
                                         {
                                             ProductID= (int)row["product_id"],
                                             ProductName = row["product_name"].ToString(),
                                             ProductPrice = (double)row["product_price"],


                                         }).ToList();
            return products;
        }
    }


}
