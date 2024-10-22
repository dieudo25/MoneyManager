using AccountService.Client.Interfaces;
using Common.Domain.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Client.Clients
{
    public class TransactionHttpClient: ITransactionClient
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public TransactionHttpClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsByAccountIdAsync(Guid accountId)
        {
            var endpointUrl = _config["API:Transaction:Endpoint:TransactionsByAccountId"];

            if (endpointUrl != null)
            {
                var response = await _httpClient.GetAsync($"{endpointUrl}/{accountId}");
                response.EnsureSuccessStatusCode();

                var transactions = await response.Content.ReadAsAsync<IEnumerable<TransactionDto>>();

                return transactions;
            }
            else
            {
                throw new NullReferenceException("Endpoint url is null, API call has failed");
            }
        }
    }
}