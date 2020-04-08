using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Model;
using ParkyAPI.Model.DTOs;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Controllers
{
    [Route("api/Trails")]
    [ApiController]
    public class TrailsController : ControllerBase
    {
        private readonly ITrailRepository _trailRepo;
        private readonly IMapper _mapper;

        public TrailsController(ITrailRepository trailRepo, IMapper mapper)
        {
            _trailRepo = trailRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of trails
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TrailDTO>))]
        public IActionResult GetTrails()
        {
            var objList = _trailRepo.GetTrails();

            var objDTO = new List<TrailDTO>();
            foreach (var item in objList)
            {
                objDTO.Add(_mapper.Map<TrailDTO>(item));
            }

            return Ok(objDTO);
        }

        /// <summary>
        /// Get individual trail
        /// </summary>
        /// <param name="trailId">The Id of trail</param>
        /// <returns></returns>
        [HttpGet("{trailId:int}", Name = "GetTrail")]
        [ProducesResponseType(200, Type = typeof(TrailDTO))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrail(int trailId)
        {
            var obj = _trailRepo.GetTrail(trailId);
            if (obj == null)
            {
                return NotFound();
            }
            TrailDTO objDTO = _mapper.Map<TrailDTO>(obj);

            return Ok(objDTO);
        }

        /// <summary>
        /// Create a trail
        /// </summary>
        /// <param name="trailDTO">The Id of trail</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TrailDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateTrail([FromBody] TrailUpsertDTO trailDTO)
        {
            if (trailDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_trailRepo.TrailExists(trailDTO.Name))
            {
                ModelState.AddModelError("", "Taril Exists!");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Trail trailObj = _mapper.Map<Trail>(trailDTO);

            if (!_trailRepo.CreateTrail(trailObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {trailDTO.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetTrail", new { trailId = trailObj.Id }, trailObj);
        }

        [HttpPatch("{trailId:int}", Name = "UpdateTrail")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTrail(int trailId, [FromBody] TrailUpsertDTO trailDTO)
        {
            if (trailDTO == null || trailDTO.Id != trailId)
            {
                return BadRequest(ModelState);
            }

            Trail trailObj = _mapper.Map<Trail>(trailDTO);

            if (!_trailRepo.UpdateTrail(trailObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }

            //return NoContent();
            return CreatedAtRoute("GetTrail", new { trailId = trailObj.Id }, trailObj);

        }

        [HttpDelete("{trailId:int}", Name = "DeleteTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult DeleteTrail(int trailId)
        {
            if (!_trailRepo.TrailExists(trailId))
            {
                return NotFound();
            }

            Trail trailObj = _trailRepo.GetTrail(trailId);

            if (!_trailRepo.DeleteTrail(trailObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}