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
    public  class WebService
    {

        IHttpClientFactory httpClientFactory = new HttpClientFactory();
        


        public HttpClient returnClient() {

            HttpClient client = httpClientFactory.CreateClient();

            client.BaseAddress = new Uri("http://localhost:18080/piOtdav-web/rest/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json", 0.8));
        return client;
            

        }

        public  void ShowProduct(Question question)
        {


            Console.WriteLine($"Name: {question.question}\tPrice: " +
                $"{question.prop1}\tCategory: {question.response}");

        }

        public  async Task<Uri> CreateProductAsync(Question question)
        {
            HttpClient client = httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.PostAsJsonAsync("question", question);
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


        public async Task<Question> GetProductAsyncByPath(string path)
        {
            HttpClient client = httpClientFactory.CreateClient();
            Question question = null;
            HttpResponseMessage response = await client.GetAsync(path);



            if (response.IsSuccessStatusCode)
            {
                question = await response.Content.ReadAsAsync<Question>();
            }
            return question;
        }
        public  async Task<IEnumerable<Question>> GetProductAsync(string path)
        {

            HttpClient client = httpClientFactory.CreateClient();
            IEnumerable<Question> listquestion = null;
            HttpResponseMessage response = await client.GetAsync(path);
           


            if (response.IsSuccessStatusCode)
            {
                listquestion = await response.Content.ReadAsAsync<IEnumerable<Question>>();
            }
            return listquestion;
        }

        public  async Task<Question> UpdateProductAsync(Question question)
        {
            HttpClient client = httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"question/{question.questionId}", question);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            question = await response.Content.ReadAsAsync<Question>();
            return question;
        }

        public  async Task<HttpStatusCode> DeleteProductAsync(int id)
        {
            HttpClient client = httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.DeleteAsync(
                $"question/{id}");
            return response.StatusCode;
        }


        /*
        public  async Task RunAsync()
        {


            // Update port # in the following line.
           

            try
            {
                // Create a new product
                Question question = new Question
                {

                    question = "question",
                    prop1 = "p1",
                    prop2 = "p2",
                    prop3 = "p3",
                    prop4 = "p4",
                    response = 1,
                    description = "desc1",
                    warningCorrectResponse = "wr"






                };

                var url = await CreateProductAsync(question);
                Console.WriteLine($"Created at {url}");

                // Get the product
                question = await GetProductAsync(url.PathAndQuery);
                ShowProduct(question);

                // Update the product
                Console.WriteLine("Updating price...");
                question.response = 2;
                await UpdateProductAsync(question);

                // Get the updated product
                question = await GetProductAsync(url.PathAndQuery);
                ShowProduct(question);

                // Delete the product
                var statusCode = await DeleteProductAsync(question.questionId);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
                
        }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/

        //  Console.ReadLine();
    }



}

