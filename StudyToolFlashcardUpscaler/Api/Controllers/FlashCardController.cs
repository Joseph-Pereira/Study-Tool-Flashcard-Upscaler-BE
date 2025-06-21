using Microsoft.AspNetCore.Mvc;
using StudyToolFlashcardUpscaler.Api.Services;
using StudyToolFlashcardUpscaler.Models.Dtos;
using StudyToolFlashcardUpscaler.Services;

namespace StudyToolFlashcardUpscaler.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlashCardController(FlashCardService flashCardService) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllFlashcards()
        {
            var response = flashCardService.GetAllFlashCards();
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetFlashcard(int id)
        {
            var response = flashCardService.GetFlashCardById(id);
            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddFlashcard([FromBody] FlashCardDto newCard)
        {
            var response = flashCardService.AddFlashCard(newCard);
            if (response == null)
                return BadRequest();

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteFlashcard(int id)
        {
            var response = flashCardService.DeleteCard(id);
            if (response == false)
            {
                return BadRequest();
            }

            return Ok("Successfuly deleted");
        }

        [HttpPut("{id}")]
public IActionResult UpdateFlashCard(int id, [FromBody] FlashCardDto updatedCard)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    updatedCard.Id = id;

    var response = flashCardService.UpdateFlashCard(updatedCard);
    if (response == null)
    {
        return NotFound($"Flashcard with ID {id} not found.");
    }

    return Ok(response);
}


    }
}