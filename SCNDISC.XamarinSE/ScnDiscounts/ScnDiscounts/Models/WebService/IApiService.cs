﻿using Refit;
using ScnDiscounts.Models.WebService.MongoDB;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScnDiscounts.Models.WebService
{
    public interface IApiService
    {
        [Get("/categories")]
        Task<List<DeserializeCategoryItem>> GetCategories(SyncParams syncParams);

        [Get("/discounts")]
        Task<List<DeserializeBranchItem>> GetDiscounts(SyncParams syncParams);

        [Get("/spatial/discounts")]
        Task<List<DeserializeBranchItem>> GetSpatials(SyncParams syncParams);

        [Get("/discounts/{id}/image")]
        Task<HttpContent> GetDiscountImage([AliasAs("id")] string documentId);

        [Get("/partners/{id}/galleryimages")]
        Task<List<string>> GetDiscountGalleryImages([AliasAs("id")] string documentId);

        [Get("/galleryimage/{id}")]
        Task<HttpContent> GetDiscountGalleryImage([AliasAs("id")] string imageId);

        [Get("/partner/rating/{id}")]
        Task<DeserializeDiscountRating> GetDiscountRating([AliasAs("id")] string documentId);

        [Get("/device/{id}/ratings")]
        Task<List<DeserializePersonalRating>> GetPersonalRatings([AliasAs("id")] string deviceId);

        [Post("/partner/rating")]
        Task<DeserializePersonalRating> PostPersonalRating([Body] DeserializePersonalRating rating);

        [Post("/feedbacks")]
        Task<HttpContent> PostFeedback([Body] DeserializeFeedback feedback);
    }
}