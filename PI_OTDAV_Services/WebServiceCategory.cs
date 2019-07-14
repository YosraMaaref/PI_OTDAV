using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Collections;
using PI_OTDAV_Services;
using PI_OTDAV_Domain;

namespace PI_OTDAV_Services
{
    public class WebServiceCategory
    {
        IHttpClientFactory httpClientFactory = new HttpClientFactory();



        public HttpClient returnClient()
        {

            HttpClient client = httpClientFactory.CreateClient();

            client.BaseAddress = new Uri("http://localhost:18080/piOtdav-web/rest/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json", 0.8));
            return client;


        }

    /*    public void ShowProduct(Question question)
        {


            Console.WriteLine($"Name: {question.question}\tPrice: " +
                $"{question.prop1}\tCategory: {question.response}");

        }
        */
        public async Task<Uri> CreateCategoryAsync(Category category)
        {
            HttpClient client = httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.PostAsJsonAsync("categories/add", category);
            String text = await response.Content.ReadAsStringAsync();
            //response.RequestMessage.RequestUri.AbsoluteUri;


            if (response.IsSuccessStatusCode)
            {

            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Uri u = new Uri(response.RequestMessage.RequestUri.AbsoluteUri + "/" + text);

            return u;


            // return URI of the created resource.

        }
       
        
     /*   public async Task<Category> GetCategory(string path)
        {
            HttpClient client = httpClientFactory.CreateClient();
            Category category = null;
            HttpResponseMessage response = await client.GetAsync(path);



            if (response.IsSuccessStatusCode)
            {
                category = await response.Content.ReadAsAsync<Category>();
            }
            return category;
        }*/
          
        public async Task<IEnumerable<Category>> GetCategory(string path)
        {

            HttpClient client = httpClientFactory.CreateClient();
            IEnumerable<Category> list = null;
            HttpResponseMessage response = await client.GetAsync(path);



            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadAsAsync<IEnumerable<Category>>();
            }
            return list;
        }
        /*
        public async Task<Question> UpdateProductAsync(Question question)
        {
            HttpClient client = httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"question/{question.questionId}", question);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            question = await response.Content.ReadAsAsync<Question>();
            return question;
        }

        public async Task<HttpStatusCode> DeleteProductAsync(int id)
        {
            HttpClient client = httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.DeleteAsync(
                $"question/{id}");
            return response.StatusCode;
        }
        */
    }



}
