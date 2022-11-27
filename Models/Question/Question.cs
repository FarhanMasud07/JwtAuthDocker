using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtAuthenticationProject.Models
{
    public class Question
    {
        public Question()
        {
            QuestionTypes = new HashSet<QuestionType>();
        }
        public Guid QuestionId { get; set; } = Guid.NewGuid();  

        public string? Title { get; set; } = null!;

        public bool CanView { get; set; } = false;

        public bool IsPremium { get; set; } = false;

        public string? Description { get; set; } = null!;

        public virtual ICollection<QuestionType> QuestionTypes { get; set; }
    }
}
