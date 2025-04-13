using Blazor_Quiz_Class;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using static Blazor_Quiz.Services.HandleError;
namespace Blazor_Quiz.Services
{
    public class Quiz_Service
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;

        public Quiz_Service(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        private async Task<bool> AddTokenToRequest()
        {
            var token = await _authService.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return true;
            }
            return false;
        }

        private async Task<bool> EnsureAuthenticated()
        {
            if (await _authService.IsTokenExpired())
            {
                if (!await _authService.RefreshToken())
                {
                    return false;
                }
            }
            return await AddTokenToRequest();
        }
        public async Task<OperationResult<List<Question>>> GetAllQuestionsAsync()
        {
            if (!await EnsureAuthenticated()) return OperationResult<List<Question>>.Failure("User is not authenticated.");
            try
            {
                var response = await _httpClient.GetAsync("Quiz/GetAllQuestions");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<List<Question>>();
                    return OperationResult<List<Question>>.Success(data);
                }
                else
                {
                    return await HandleErrorResponse<List<Question>>(response);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<List<Question>>.Failure("An error occurred while fetching questions: " + ex.Message);
            }
        }

        public async Task<OperationResult<Question>> CreateQuestionAsync(Question question)
        {
            if (!await EnsureAuthenticated()) return OperationResult<Question>.Failure("User is not authenticated.");
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Quiz/CreateQuestion", question);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<Question>();
                    return OperationResult<Question>.Success(data);
                }
                else
                {
                    return await HandleErrorResponse<Question>(response);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<Question>.Failure("An error occurred while creating a question: " + ex.Message);
            }
        }
        public async Task<OperationResult<UserQuizResult>> CreateResultAsync(UserQuizResult result)
        {
            if (!await EnsureAuthenticated()) return OperationResult<UserQuizResult>.Failure("User is not authenticated.");
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Quiz/CreateResult", result);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<UserQuizResult>();
                    return OperationResult<UserQuizResult>.Success(data);
                }
                else
                {
                    return await HandleErrorResponse<UserQuizResult>(response);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<UserQuizResult>.Failure("An error occurred while creating a result: " + ex.Message);
            }
        }
        public async Task<OperationResult<UserAnswer>> CreateUserAnswerAsync(UserAnswer answer)
        {
            if (!await EnsureAuthenticated()) return OperationResult<UserAnswer>.Failure("User is not authenticated.");
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Quiz/CreateUserAnswer", answer);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<UserAnswer>();
                    return OperationResult<UserAnswer>.Success(data);
                }
                else
                {
                    return await HandleErrorResponse<UserAnswer>(response);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<UserAnswer>.Failure("An error occurred while creating: " + ex.Message);
            }
        }

        public async Task<OperationResult<bool>> UpdateUserAnswerAsync(int id, UserAnswer question)
        {
            if (!await EnsureAuthenticated()) return OperationResult<bool>.Failure("User is not authenticated.");
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"Quiz/UpdateUserAnswer/{id}", question);
                if (response.IsSuccessStatusCode)
                {
                    return OperationResult<bool>.Success(true);
                }
                else
                {
                    return await HandleErrorResponse<bool>(response);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure("An error occurred while updating: " + ex.Message);
            }
        }

        public async Task<OperationResult<bool>> UpdateResultsync(int id, UserQuizResult result)
        {
            if (!await EnsureAuthenticated()) return OperationResult<bool>.Failure("User is not authenticated.");
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"Quiz/UpdateResult/{id}", result);
                if (response.IsSuccessStatusCode)
                {
                    return OperationResult<bool>.Success(true);
                }
                else
                {
                    return await HandleErrorResponse<bool>(response);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure("An error occurred while updating: " + ex.Message);
            }
        }
        public async Task<OperationResult<bool>> UpdateQuestionAsync(int id, Question question)
        {
            if (!await EnsureAuthenticated()) return OperationResult<bool>.Failure("User is not authenticated.");
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"Quiz/UpdateQuestion/{id}", question);
                if (response.IsSuccessStatusCode)
                {
                    return OperationResult<bool>.Success(true);
                }
                else
                {
                    return await HandleErrorResponse<bool>(response);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure("An error occurred while updating the question: " + ex.Message);
            }
        }

        public async Task<OperationResult<bool>> DeleteQuestionAsync(int id)
        {
            if (!await EnsureAuthenticated()) return OperationResult<bool>.Failure("User is not authenticated.");
            try
            {
                var response = await _httpClient.DeleteAsync($"Quiz/DeleteQuestion/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return OperationResult<bool>.Success(true);
                }
                else
                {
                    return await HandleErrorResponse<bool>(response);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure("An error occurred while deleting the question: " + ex.Message);
            }
        }

        public async Task<OperationResult<List<UserQuizResult>>> GetAllResultsAsync()
        {
            if (!await EnsureAuthenticated()) return OperationResult<List<UserQuizResult>>.Failure("User is not authenticated.");
            try
            {
                var response = await _httpClient.GetAsync("Quiz/GetAllResult");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<List<UserQuizResult>>();
                    return OperationResult<List<UserQuizResult>>.Success(data);
                }
                else
                {
                    return await HandleErrorResponse<List<UserQuizResult>>(response);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<List<UserQuizResult>>.Failure("An error occurred while fetching results: " + ex.Message);
            }
        }

        public async Task<OperationResult<Quiz_Main>> GetQuizAndQuestions(int id)
        {
            if (!await EnsureAuthenticated()) return OperationResult<Quiz_Main>.Failure("User is not authenticated.");
            try
            {
                var response = await _httpClient.GetAsync($"Quiz/GetQuizAndQuestions/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<Quiz_Main>();
                    return OperationResult<Quiz_Main>.Success(data);
                }
                else
                {
                    return await HandleErrorResponse<Quiz_Main>(response);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<Quiz_Main>.Failure("An error occurred while fetching the result by id: " + ex.Message);
            }
        }

        public async Task<OperationResult<List<Quiz_Main>>> GetAllQuiz()
        {
            if (!await EnsureAuthenticated()) return OperationResult<List<Quiz_Main>>.Failure("User is not authenticated.");
            try
            {
                var response = await _httpClient.GetAsync($"Quiz/GetAllQuiz");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<List<Quiz_Main>>();
                    return OperationResult<List<Quiz_Main>>.Success(data);
                }
                else
                {
                    return await HandleErrorResponse<List<Quiz_Main>>(response);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<List<Quiz_Main>>.Failure("An error occurred while fetching the result by id: " + ex.Message);
            }
        }

        public async Task<OperationResult<Quiz_Main>> CreateQuiz(Quiz_Main answer)
        {
            if (!await EnsureAuthenticated()) return OperationResult<Quiz_Main>.Failure("User is not authenticated.");
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Quiz/CreateQuiz", answer);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<Quiz_Main>();
                    return OperationResult<Quiz_Main>.Success(data);
                }
                else
                {
                    return await HandleErrorResponse<Quiz_Main>(response);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<Quiz_Main>.Failure("An error occurred while creating: " + ex.Message);
            }
        }

        public async Task<OperationResult<bool>> UpdateQuiz(int id, Quiz_Main question)
        {
            if (!await EnsureAuthenticated()) return OperationResult<bool>.Failure("User is not authenticated.");
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"Quiz/UpdateQuiz/{id}", question);
                if (response.IsSuccessStatusCode)
                {
                    return OperationResult<bool>.Success(true);
                }
                else
                {
                    return await HandleErrorResponse<bool>(response);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure("An error occurred while updating the question: " + ex.Message);
            }
        }
        public async Task<OperationResult<UserQuizResult>> GetResultByIdAsync(int id)
        {
            if (!await EnsureAuthenticated()) return OperationResult<UserQuizResult>.Failure("User is not authenticated.");
            try
            {
                var response = await _httpClient.GetAsync($"Quiz/GetResultById/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<UserQuizResult>();
                    return OperationResult<UserQuizResult>.Success(data);
                }
                else
                {
                    return await HandleErrorResponse<UserQuizResult>(response);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<UserQuizResult>.Failure("An error occurred while fetching the result by id: " + ex.Message);
            }
        }
    }
}
