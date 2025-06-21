// Services/NoteService.cs
using Microsoft.AspNetCore.Mvc;
using StudyToolFlashcardUpscaler.Api.Services;
using StudyToolFlashcardUpscaler.Models.Dtos;

namespace StudyToolFlashcardUpscaler.Services
{
    public class FlashCardService
    {

        private readonly DatabaseService _database;


        public FlashCardService(DatabaseService database)
        {
            _database = database;
            _database.LoadData();
        }

        public IEnumerable<FlashCardDto> GetAllFlashCards()
        {
            return _database.GetCards();
        }

        public FlashCardDto? GetFlashCardById(int id)
        {
            if (_database.Data == null)
                return null;

            var card = _database.Data.cards!.FirstOrDefault(x => x.Id == id);
            if (card != null)
            {
                return card;
            }
            else
            {
                return null;
            }
        }

        public FlashCardDto AddFlashCard(FlashCardDto newFlashCard)
        {
            if (_database.Data == null)
                return null;

            if (_database.Data.cards == null)
                _database.Data.cards = new List<FlashCardDto>();

            var highestId = 0;
            if (_database.Data.cards.Count > 0)
                highestId = _database.Data.cards.Max(x => x.Id);

            newFlashCard.Id = highestId + 1;

            _database.Data.cards.Add(newFlashCard);

            _database.SaveData();
            return newFlashCard;
        }

        public bool DeleteCard(int id)
        {
            var card = _database.Data?.cards?.FirstOrDefault(n => n.Id == id);
            if (card == null)
                return false;

            _database.Data.cards.Remove(card);
            _database.SaveData();

            return true;
        }

        public FlashCardDto? UpdateFlashCard(FlashCardDto updatedFlashCard)
        {
            if (_database.Data == null || _database.Data.cards == null)
                return null;

            var existingCard = _database.Data.cards.FirstOrDefault(card => card.Id == updatedFlashCard.Id);

            if (existingCard == null)
                return null;

            // Update the properties
            existingCard.Question = updatedFlashCard.Question;
            existingCard.Answer = updatedFlashCard.Answer;
            existingCard.Options = updatedFlashCard.Options;

            _database.SaveData();
            return existingCard;
        }


    }
}
