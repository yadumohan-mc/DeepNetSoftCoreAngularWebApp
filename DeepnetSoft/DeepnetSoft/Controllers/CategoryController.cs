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

        [HttpGet("{id?}")]
        public CategoryProductViewModel Get(string id="0")
        {
            CategoryProductViewModel categoryViewModel = new CategoryProductViewModel();
            categoryViewModel.SubCategories = GetSubCategoriesByCategoryID(id);
            categoryViewModel.Products = GetProductsByCategoryID(id);
            categoryViewModel.Category = GetCategoryByCategoryID(id);
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
                                                CategoryName = row["category_name"].ToString(),
                                                  ProductCount = GetProductCountByID(row["category_id"].ToString())
                                            }).ToList();
            return  categories;
        }
        private Category GetCategoryByCategoryID(string id)
        {
            if (id == "0")
            {
                return new Category
                {
                    CategoryID = 0,
                    CategoryName = "Category",
                    ProductCount = GetProductCountByID("0"),
                };
            }
            string querry = @"SELECT * FROM dbo.category WHERE category_id = " + id + @"";
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
            Category category = (from DataRow row in dataTable.Rows

                                 select new Category
                                 {
                                     CategoryID = (int)row["category_id"],
                                     CategoryName = row["category_name"].ToString(),
                                     ProductCount = GetProductCountByID(id)
                                 }).FirstOrDefault();
            return category;
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
        private int GetProductCountByID(string id)
        {
            string querry;
            if (id == "0")
            {
                querry = @"Select product_id from dbo.product";
            }
            else
            {
                querry = @"Select * from dbo.product where category_id in(
SELECT category_id FROM dbo.category
WHERE hierarchal_order LIKE '%/" + id + @"/%'or category_id="+id+@");";
            }
            DataTable dataTable = new DataTable();
            int result = 0;
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
            result = dataTable.Rows.Count;

                  
            return result;
        }
    }


}
