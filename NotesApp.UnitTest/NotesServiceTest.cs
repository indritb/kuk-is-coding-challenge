using Moq;
using NotesApp.Lib.Shared;
using NotesApp.Lib.UnitOfWork.UnitOfWork;
using NotesApp.WebAPI.Implementation.Business.NotesManagement.Service;
using NotesApp.WebAPI.Implementation.Domain;
using NotesApp.WebAPI.Implementation.Domain.Entities;
using NotesApp.WebAPI.Implementation.Domain.RepositoryInterfaces;
using System.Linq.Expressions;

namespace NotesApp.UnitTest
{
    public class NotesServiceTest
    {
        private readonly Mock<IUnitOfWork<NotesContext>> _uowMock;
        private readonly Mock<INotesRepository> _repositoryMock;
        private readonly INotesService _systemUnderTest;

        public NotesServiceTest()
        {
            _uowMock = new();
            _repositoryMock = new();
            _uowMock.Setup(x => x.Repository<INotesRepository>()).Returns(_repositoryMock.Object);
            _systemUnderTest = new NotesService(_uowMock.Object);
        }

        [Fact]
        public async void NotesService_ShouldReturnStoredNotes()
        {
            var notes = new List<Note>
            {
                new Note
                {
                    Id = 1,
                    Title = "Cooking",
                    Description = "Cook pasta tomorrow",
                    Type = NoteType.Normal
                },
                new Note
                {
                    Id = 2,
                    Title = "Car Repair",
                    Description = "Repair flat tyres",
                    Type = NoteType.Important
                }
            };

            _repositoryMock.Setup(x => x.Get(It.IsAny<Expression<Func<Note, bool>>>())).ReturnsAsync(notes).Verifiable();

            var result = await _systemUnderTest.GetAllNotes().ConfigureAwait(false);

            _repositoryMock.Verify();

            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}