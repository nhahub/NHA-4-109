using BusinessLogicLayer.Interfaces;
using DataAccessLayer.ModelContetxt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.DTOs;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [AllowAnonymous]
        [HttpGet("getAllReviews")]
        public IActionResult GetAll()
        {
            var reviews = _reviewRepository.GetAll();

            var result = reviews
                .Where(r => !r.IsDeleted)
                .Select(r => new ReviewDTO
                {
                    ReviewID = r.ReviewID,
                    Content = r.Content,
                    ReciveDate = r.ReciveDate,
                    ReadDate = r.ReadDate,

                    PropertyId = r.PropertyId,
                    TenantId = r.TenantId
                });

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("GetReviewBy{id}")]
        public IActionResult GetById(int id)
        {
            var review = _reviewRepository.GetById(id);

            if (review == null)
                return NotFound();

            var dto = new ReviewDTO
            {
                ReviewID = review.ReviewID,
                Content = review.Content,
                ReciveDate = review.ReciveDate,
                ReadDate = review.ReadDate,
                
                PropertyId = review.PropertyId,
                TenantId = review.TenantId
            };

            return Ok(dto);
        }

        [AllowAnonymous]
        [HttpGet("GetpropertyReviews/{propertyId}")]
        public IActionResult GetReviewsByProperty(int propertyId)
        {
            var reviews = _reviewRepository.GetReviewsByProperty(propertyId);

            var result = reviews.Select(r => new ReviewDTO
            {
                ReviewID = r.ReviewID,
                Content = r.Content,
                ReciveDate = r.ReciveDate,
                ReadDate = r.ReadDate,
                
                PropertyId = r.PropertyId,
                TenantId = r.TenantId
            });

            return Ok(result);
        }

        // GET: api/Review/Gettenant/1
        [Authorize(Roles = "Admin,Tenant")]
        [HttpGet("GettenantReviews/{tenantId}")]
        public IActionResult GetReviewsByTenant(int tenantId)
        {
            var reviews = _reviewRepository.GetReviewsByTenant(tenantId);

            var result = reviews.Select(r => new ReviewDTO
            {
                ReviewID = r.ReviewID,
                Content = r.Content,
                ReciveDate = r.ReciveDate,
                ReadDate = r.ReadDate,
              
                PropertyId = r.PropertyId,
                TenantId = r.TenantId
            });

            return Ok(result);
        }

        [Authorize(Roles = "Tenant")]
        [HttpPost("AddReview")]
        public IActionResult Add(ReviewDTO dto)
        {
            var review = new Review
            {
                Content = dto.Content,
                ReciveDate = dto.ReciveDate,
                ReadDate = dto.ReadDate,
                
                PropertyId = dto.PropertyId,
                TenantId = dto.TenantId
            };

            _reviewRepository.Add(review);
            _reviewRepository.Save();

            return Ok("Added Successfully");
        }

        [Authorize(Roles = "Tenant")]
        [HttpPut("UpdateReview/{id}")]
        public IActionResult Update(int id, ReviewDTO dto)
        {
            var review = _reviewRepository.GetById(id);

            if (review == null)
                return NotFound();

            review.Content = dto.Content;
            review.ReciveDate = dto.ReciveDate;
            review.ReadDate = dto.ReadDate;
           
            review.PropertyId = dto.PropertyId;
            review.TenantId = dto.TenantId;

            _reviewRepository.Update(review);
            _reviewRepository.Save();

            return Ok("Review updated successfully.");
        }

        [Authorize(Roles = "Admin,Tenant")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var review = _reviewRepository.GetById(id);

            if (review == null)
                return NotFound();

            _reviewRepository.SoftDelete(review);
            _reviewRepository.Save();

            return Ok("Review deleted successfully.");
        }
    }
}