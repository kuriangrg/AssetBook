
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

public static class UrlExtensions
{
    /// <summary>
    /// Generates a fully qualified URL to the specified content by using the specified content path.
    /// </summary>
    /// <param name="url">The URL helper.</param>
    /// <param name="contentPath">The content path.</param>
    /// <returns>The absolute URL.</returns>
    public static string GetBaseUrl(this HttpRequest request, string contentPath)
    {
        
        return (request.Scheme + "://" + request.Host.Value+contentPath);
    }
}