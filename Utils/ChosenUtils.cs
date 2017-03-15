using Application.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Utils {
    public class ChosenUtils {
        public static string GetQuery(HttpRequest Request) {
            return Request.Form["data[q]"];
        }

        public static JsonResult GetResponse(string query, ChosenAutoCompleteResult[] results) {
            return new JsonResult(new
            {
                q = query,
                results = results
            });
        }
    }
}