using System.ComponentModel.DataAnnotations;

namespace JwtAuthenticationProject.Models
{
    public class QuestionType
    {
        public QuestionType()
        {
            Questions = new HashSet<Question>();
        }
        public Guid QuestionTypeId { get; set; } = Guid.NewGuid();
        public string QuestionTypeName { get; set; } = string.Empty;
        public virtual ICollection<Question> Questions { get; set; }
    }
}
