using TaekwondoOrchestration.ApiService.DTO;
using TaekwondoOrchestration.ApiService.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class QuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly ISpørgsmålRepository _spørgsmålRepository;
        private readonly IBrugerQuizRepository _brugerQuizRepository;
        private readonly IKlubQuizRepository _klubQuizRepository;

        public QuizService(IQuizRepository quizRepository, ISpørgsmålRepository spørgsmålRepository, IBrugerQuizRepository brugerQuizRepository, IKlubQuizRepository klubQuizRepository)
        {
            _quizRepository = quizRepository;
            _spørgsmålRepository = spørgsmålRepository;
            _brugerQuizRepository = brugerQuizRepository;
            _klubQuizRepository = klubQuizRepository;
        }

        // Get all quizzes as DTO
        public async Task<List<QuizDTO>> GetAllQuizzesAsync()
        {
            var quizzes = await _quizRepository.GetAllAsync();

            return quizzes.Select(q => new QuizDTO
            {
                QuizID = q.QuizID,
                QuizNavn = q.QuizNavn,
                QuizBeskrivelse = q.QuizBeskrivelse,
                PensumID = q.PensumID,
                Spørgsmål = q.Spørgsmåls.Select(s => new SpørgsmålDTO
                {
                    SpørgsmålID = s.SpørgsmålID,
                    SpørgsmålRækkefølge = s.SpørgsmålRækkefølge,
                    SpørgsmålTid = s.SpørgsmålTid,
                    Teknik = s.Teknik != null ? new TeknikDTO
                    {
                        TeknikID = s.Teknik.TeknikID,
                        TeknikNavn = s.Teknik.TeknikNavn,
                        TeknikBeskrivelse = s.Teknik.TeknikBeskrivelse,
                        TeknikBillede = s.Teknik.TeknikBillede,
                        TeknikVideo = s.Teknik.TeknikVideo,
                        TeknikLyd = s.Teknik.TeknikLyd,
                        PensumID = s.Teknik.PensumID
                    } : null,
                    Teori = s.Teori != null ? new TeoriDTO
                    {
                        TeoriID = s.Teori.TeoriID,
                        TeoriNavn = s.Teori.TeoriNavn,
                        TeoriBeskrivelse = s.Teori.TeoriBeskrivelse,
                        TeoriBillede = s.Teori.TeoriBillede,
                        TeoriVideo = s.Teori.TeoriVideo,
                        TeoriLyd = s.Teori.TeoriLyd,
                        PensumID = s.Teori.PensumID
                    } : null,
                    Øvelse = s.Øvelse != null ? new ØvelseDTO
                    {
                        ØvelseID = s.Øvelse.ØvelseID,
                        ØvelseNavn = s.Øvelse.ØvelseNavn,
                        ØvelseBeskrivelse = s.Øvelse.ØvelseBeskrivelse,
                        ØvelseBillede = s.Øvelse.ØvelseBillede,
                        ØvelseVideo = s.Øvelse.ØvelseVideo,
                        ØvelseTid = s.Øvelse.ØvelseTid,
                        ØvelseSværhed = s.Øvelse.ØvelseSværhed,
                    } : null
                }).ToList()
            }).ToList();
        }

        // Get quiz by ID
        public async Task<QuizDTO?> GetQuizByIdAsync(int id)
        {
            var quiz = await _quizRepository.GetByIdAsync(id);
            if (quiz == null)
                return null;

            return new QuizDTO
            {
                QuizID = quiz.QuizID,
                QuizNavn = quiz.QuizNavn,
                QuizBeskrivelse = quiz.QuizBeskrivelse,
                PensumID = quiz.PensumID,
                Spørgsmål = quiz.Spørgsmåls.Select(s => new SpørgsmålDTO
                {
                    SpørgsmålID = s.SpørgsmålID,
                    SpørgsmålRækkefølge = s.SpørgsmålRækkefølge,
                    SpørgsmålTid = s.SpørgsmålTid,
                    Teknik = s.Teknik != null ? new TeknikDTO
                    {
                        TeknikID = s.Teknik.TeknikID,
                        TeknikNavn = s.Teknik.TeknikNavn,
                        TeknikBeskrivelse = s.Teknik.TeknikBeskrivelse,
                        TeknikBillede = s.Teknik.TeknikBillede,
                        TeknikVideo = s.Teknik.TeknikVideo,
                        TeknikLyd = s.Teknik.TeknikLyd,
                        PensumID = s.Teknik.PensumID
                    } : null,
                    Teori = s.Teori != null ? new TeoriDTO
                    {
                        TeoriID = s.Teori.TeoriID,
                        TeoriNavn = s.Teori.TeoriNavn,
                        TeoriBeskrivelse = s.Teori.TeoriBeskrivelse,
                        TeoriBillede = s.Teori.TeoriBillede,
                        TeoriVideo = s.Teori.TeoriVideo,
                        TeoriLyd = s.Teori.TeoriLyd,
                        PensumID = s.Teori.PensumID
                    } : null,
                    Øvelse = s.Øvelse != null ? new ØvelseDTO
                    {
                        ØvelseID = s.Øvelse.ØvelseID,
                        ØvelseNavn = s.Øvelse.ØvelseNavn,
                        ØvelseBeskrivelse = s.Øvelse.ØvelseBeskrivelse,
                        ØvelseBillede = s.Øvelse.ØvelseBillede,
                        ØvelseVideo = s.Øvelse.ØvelseVideo,
                        ØvelseTid = s.Øvelse.ØvelseTid,
                        ØvelseSværhed = s.Øvelse.ØvelseSværhed,
                    } : null
                }).ToList()
            };
        }

        // Create quiz based on DTO
        public async Task<QuizDTO> CreateQuizAsync(QuizDTO quizDto)
        {
            var newQuiz = new Quiz
            {
                QuizNavn = quizDto.QuizNavn,
                QuizBeskrivelse = quizDto.QuizBeskrivelse,
                PensumID = quizDto.PensumID
            };

            var createdQuiz = await _quizRepository.CreateAsync(newQuiz);
            return new QuizDTO
            {
                QuizID = createdQuiz.QuizID,
                QuizNavn = createdQuiz.QuizNavn,
                QuizBeskrivelse = createdQuiz.QuizBeskrivelse,
                PensumID = createdQuiz.PensumID
            };
        }

        // Update quiz based on DTO
        public async Task<bool> UpdateQuizAsync(int id, QuizDTO quizDto)
        {
            if (id != quizDto.QuizID) return false;

            var existingQuiz = await _quizRepository.GetByIdAsync(id);
            if (existingQuiz == null) return false;

            if (string.IsNullOrEmpty(quizDto.QuizNavn) || string.IsNullOrEmpty(quizDto.QuizBeskrivelse))
            {
                return false; // Invalid input
            }

            existingQuiz.QuizNavn = quizDto.QuizNavn;
            existingQuiz.QuizBeskrivelse = quizDto.QuizBeskrivelse;
            existingQuiz.PensumID = quizDto.PensumID;

            return await _quizRepository.UpdateAsync(existingQuiz);
        }

        // Delete quiz by ID
        public async Task<bool> DeleteQuizAsync(int id)
        {
            return await _quizRepository.DeleteAsync(id);
        }
        public async Task<QuizDTO> CreateQuizWithBrugerAndKlubAsync(QuizDTO dto)
        {
            // Create Quiz object
            var quiz = new Quiz
            {
                QuizNavn = dto.QuizNavn,
                QuizBeskrivelse = dto.QuizBeskrivelse,
                PensumID = dto.PensumID
            };

            // Save Quiz first to get QuizID
            var createdQuiz = await _quizRepository.CreateAsync(quiz);

            // Validate BrugerID (User ID)
            if (dto.BrugerID != null && dto.BrugerID != 0)
            {
                var brugerQuiz = new BrugerQuiz
                {
                    BrugerID = (int)dto.BrugerID,
                    QuizID = createdQuiz.QuizID,
                };
                await _brugerQuizRepository.CreateBrugerQuizAsync(brugerQuiz);
            }

            // Validate KlubID (Club ID)
            if (dto.KlubID != null && dto.KlubID != 0)
            {
                var klubQuiz = new KlubQuiz
                {
                    KlubID = (int)dto.KlubID,
                    QuizID = createdQuiz.QuizID,
                };
                await _klubQuizRepository.CreateKlubQuizAsync(klubQuiz);
            }

            // Add associated Spørgsmål (questions) to the quiz
            foreach (var spørgsmål in dto.Spørgsmål)
            {
                var newSpørgsmål = new Spørgsmål
                {
                    QuizID = createdQuiz.QuizID,
                    TeoriID = spørgsmål.Teori.TeoriID,
                    TeknikID = spørgsmål.Teknik.TeknikID,
                    ØvelseID = spørgsmål.Øvelse.ØvelseID,
                    SpørgsmålTid = spørgsmål.SpørgsmålTid,
                    SpørgsmålRækkefølge = spørgsmål.SpørgsmålRækkefølge
                };

                // Save the Spørgsmål record to the database
                await _spørgsmålRepository.CreateAsync(newSpørgsmål);
            }

            return dto;
        }
        public async Task<QuizDTO> UpdateQuizWithBrugerAndKlubAsync(int quizId, QuizDTO dto)
        {
            var existingQuiz = await _quizRepository.GetByIdAsync(quizId);
            if (existingQuiz == null)
                return null; // Handle not found case

            // Update Quiz fields
            existingQuiz.QuizNavn = dto.QuizNavn;
            existingQuiz.QuizBeskrivelse = dto.QuizBeskrivelse;
            existingQuiz.PensumID = dto.PensumID;

            await _quizRepository.UpdateAsync(existingQuiz);

            //// Update Bruger association
            //if (dto.BrugerID != null && dto.BrugerID != 0)
            //{
            //    var existingBrugerQuiz = await _brugerQuizRepository.GetByQuizIdAsync(quizId);
            //    if (existingBrugerQuiz == null)
            //    {
            //        await _brugerQuizRepository.CreateBrugerQuizAsync(new BrugerQuiz
            //        {
            //            BrugerID = dto.BrugerID,
            //            QuizID = quizId
            //        });
            //    }
            //    else
            //    {
            //        existingBrugerQuiz.BrugerID = dto.BrugerID;
            //        await _brugerQuizRepository.UpdateBrugerQuizAsync(existingBrugerQuiz);
            //    }
            //}

            //// Update Klub association
            //if (dto.KlubID != null && dto.KlubID != 0)
            //{
            //    var existingKlubQuiz = await _klubQuizRepository.GetByQuizIdAsync(quizId);
            //    if (existingKlubQuiz == null)
            //    {
            //        await _klubQuizRepository.CreateKlubQuizAsync(new KlubQuiz
            //        {
            //            KlubID = dto.KlubID,
            //            QuizID = quizId
            //        });
            //    }
            //    else
            //    {
            //        existingKlubQuiz.KlubID = dto.KlubID;
            //        await _klubQuizRepository.(existingKlubQuiz);
            //    }
            //}


            // Update Spørgsmål
            var existingSpørgsmåls = await _spørgsmålRepository.GetByQuizIdAsync(quizId);

            // Convert existing Spørgsmål to a dictionary for easy lookup
            var spørgsmålDictionary = existingSpørgsmåls.ToDictionary(s => s.SpørgsmålID);

            // Loop through new spørgsmål
            foreach (var spørgsmålDTO in dto.Spørgsmål)
            {
                if (spørgsmålDictionary.TryGetValue(spørgsmålDTO.SpørgsmålID, out var existingSpørgsmål))
                {
                    // Update existing spørgsmål
                    existingSpørgsmål.TeoriID = spørgsmålDTO.Teori.TeoriID;
                    existingSpørgsmål.TeknikID = spørgsmålDTO.Teknik.TeknikID;
                    existingSpørgsmål.ØvelseID = spørgsmålDTO.Øvelse.ØvelseID;
                    existingSpørgsmål.SpørgsmålTid = spørgsmålDTO.SpørgsmålTid;
                    existingSpørgsmål.SpørgsmålRækkefølge = spørgsmålDTO.SpørgsmålRækkefølge;

                    await _spørgsmålRepository.UpdateAsync(existingSpørgsmål);
                }
                else
                {
                    // Insert new spørgsmål
                    await _spørgsmålRepository.CreateAsync(new Spørgsmål
                    {
                        QuizID = quizId,
                        TeoriID = spørgsmålDTO.Teori.TeoriID,
                        TeknikID = spørgsmålDTO.Teknik.TeknikID,
                        ØvelseID = spørgsmålDTO.Øvelse.ØvelseID,
                        SpørgsmålTid = spørgsmålDTO.SpørgsmålTid,
                        SpørgsmålRækkefølge = spørgsmålDTO.SpørgsmålRækkefølge
                    });
                }
            }

            // Remove spørgsmål that are not in the new list
            var newSpørgsmålIds = dto.Spørgsmål.Select(s => s.SpørgsmålID).ToHashSet();
            foreach (var existing in existingSpørgsmåls)
            {
                if (!newSpørgsmålIds.Contains(existing.SpørgsmålID))
                {
                    await _spørgsmålRepository.DeleteAsync(existing.SpørgsmålID);
                }
            }

            return dto;

        }
        public async Task<List<QuizDTO>> GetAllQuizzesByBrugerAsync(int brugerId)
        {
            var quizzes = await _quizRepository.GetAllByBrugerAsync(brugerId);

            return quizzes.Select(q => new QuizDTO
            {
                QuizID = q.QuizID,
                QuizNavn = q.QuizNavn,
                QuizBeskrivelse = q.QuizBeskrivelse,
                PensumID = q.PensumID,
                Spørgsmål = q.Spørgsmåls.Select(s => new SpørgsmålDTO
                {
                    SpørgsmålID = s.SpørgsmålID,
                    SpørgsmålRækkefølge = s.SpørgsmålRækkefølge,
                    SpørgsmålTid = s.SpørgsmålTid,
                    Teknik = s.Teknik != null ? new TeknikDTO
                    {
                        TeknikID = s.Teknik.TeknikID,
                        TeknikNavn = s.Teknik.TeknikNavn,
                        TeknikBeskrivelse = s.Teknik.TeknikBeskrivelse,
                        TeknikBillede = s.Teknik.TeknikBillede,
                        TeknikVideo = s.Teknik.TeknikVideo,
                        TeknikLyd = s.Teknik.TeknikLyd,
                        PensumID = s.Teknik.PensumID
                    } : null,
                    Teori = s.Teori != null ? new TeoriDTO
                    {
                        TeoriID = s.Teori.TeoriID,
                        TeoriNavn = s.Teori.TeoriNavn,
                        TeoriBeskrivelse = s.Teori.TeoriBeskrivelse,
                        TeoriBillede = s.Teori.TeoriBillede,
                        TeoriVideo = s.Teori.TeoriVideo,
                        TeoriLyd = s.Teori.TeoriLyd,
                        PensumID = s.Teori.PensumID
                    } : null,
                    Øvelse = s.Øvelse != null ? new ØvelseDTO
                    {
                        ØvelseID = s.Øvelse.ØvelseID,
                        ØvelseNavn = s.Øvelse.ØvelseNavn,
                        ØvelseBeskrivelse = s.Øvelse.ØvelseBeskrivelse,
                        ØvelseBillede = s.Øvelse.ØvelseBillede,
                        ØvelseVideo = s.Øvelse.ØvelseVideo,
                        ØvelseTid = s.Øvelse.ØvelseTid,
                        ØvelseSværhed = s.Øvelse.ØvelseSværhed,
                    } : null
                }).ToList()
            }).ToList();
        }
        public async Task<List<QuizDTO>> GetAllQuizzesByKlubAsync(int klubId)
        {
            var quizzes = await _quizRepository.GetAllByKlubAsync(klubId);

            return quizzes.Select(q => new QuizDTO
            {
                QuizID = q.QuizID,
                QuizNavn = q.QuizNavn,
                QuizBeskrivelse = q.QuizBeskrivelse,
                PensumID = q.PensumID,
                Spørgsmål = q.Spørgsmåls.Select(s => new SpørgsmålDTO
                {
                    SpørgsmålID = s.SpørgsmålID,
                    SpørgsmålRækkefølge = s.SpørgsmålRækkefølge,
                    SpørgsmålTid = s.SpørgsmålTid,
                    Teknik = s.Teknik != null ? new TeknikDTO
                    {
                        TeknikID = s.Teknik.TeknikID,
                        TeknikNavn = s.Teknik.TeknikNavn,
                        TeknikBeskrivelse = s.Teknik.TeknikBeskrivelse,
                        TeknikBillede = s.Teknik.TeknikBillede,
                        TeknikVideo = s.Teknik.TeknikVideo,
                        TeknikLyd = s.Teknik.TeknikLyd,
                        PensumID = s.Teknik.PensumID
                    } : null,
                    Teori = s.Teori != null ? new TeoriDTO
                    {
                        TeoriID = s.Teori.TeoriID,
                        TeoriNavn = s.Teori.TeoriNavn,
                        TeoriBeskrivelse = s.Teori.TeoriBeskrivelse,
                        TeoriBillede = s.Teori.TeoriBillede,
                        TeoriVideo = s.Teori.TeoriVideo,
                        TeoriLyd = s.Teori.TeoriLyd,
                        PensumID = s.Teori.PensumID
                    } : null,
                    Øvelse = s.Øvelse != null ? new ØvelseDTO
                    {
                        ØvelseID = s.Øvelse.ØvelseID,
                        ØvelseNavn = s.Øvelse.ØvelseNavn,
                        ØvelseBeskrivelse = s.Øvelse.ØvelseBeskrivelse,
                        ØvelseBillede = s.Øvelse.ØvelseBillede,
                        ØvelseVideo = s.Øvelse.ØvelseVideo,
                        ØvelseTid = s.Øvelse.ØvelseTid,
                        ØvelseSværhed = s.Øvelse.ØvelseSværhed,
                    } : null
                }).ToList()
            }).ToList();
        }
        public async Task<List<QuizDTO>> GetAllQuizzesByPensumAsync(int pensumId)
        {
            var quizzes = await _quizRepository.GetAllByPensumAsync(pensumId);

            return quizzes.Select(q => new QuizDTO
            {
                QuizID = q.QuizID,
                QuizNavn = q.QuizNavn,
                QuizBeskrivelse = q.QuizBeskrivelse,
                PensumID = q.PensumID,
                Spørgsmål = q.Spørgsmåls.Select(s => new SpørgsmålDTO
                {
                    SpørgsmålID = s.SpørgsmålID,
                    SpørgsmålRækkefølge = s.SpørgsmålRækkefølge,
                    SpørgsmålTid = s.SpørgsmålTid,
                    Teknik = s.Teknik != null ? new TeknikDTO
                    {
                        TeknikID = s.Teknik.TeknikID,
                        TeknikNavn = s.Teknik.TeknikNavn,
                        TeknikBeskrivelse = s.Teknik.TeknikBeskrivelse,
                        TeknikBillede = s.Teknik.TeknikBillede,
                        TeknikVideo = s.Teknik.TeknikVideo,
                        TeknikLyd = s.Teknik.TeknikLyd,
                        PensumID = s.Teknik.PensumID
                    } : null,
                    Teori = s.Teori != null ? new TeoriDTO
                    {
                        TeoriID = s.Teori.TeoriID,
                        TeoriNavn = s.Teori.TeoriNavn,
                        TeoriBeskrivelse = s.Teori.TeoriBeskrivelse,
                        TeoriBillede = s.Teori.TeoriBillede,
                        TeoriVideo = s.Teori.TeoriVideo,
                        TeoriLyd = s.Teori.TeoriLyd,
                        PensumID = s.Teori.PensumID
                    } : null,
                    Øvelse = s.Øvelse != null ? new ØvelseDTO
                    {
                        ØvelseID = s.Øvelse.ØvelseID,
                        ØvelseNavn = s.Øvelse.ØvelseNavn,
                        ØvelseBeskrivelse = s.Øvelse.ØvelseBeskrivelse,
                        ØvelseBillede = s.Øvelse.ØvelseBillede,
                        ØvelseVideo = s.Øvelse.ØvelseVideo,
                        ØvelseTid = s.Øvelse.ØvelseTid,
                        ØvelseSværhed = s.Øvelse.ØvelseSværhed,
                    } : null
                }).ToList()
            }).ToList();
        }

    }

}
