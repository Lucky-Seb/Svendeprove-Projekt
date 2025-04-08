using Api.DTOs;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class ProgramPlanService
    {
        private readonly IProgramPlanRepository _programPlanRepository;
        private readonly ITræningRepository _træningRepository;
        private readonly IKlubProgramRepository _klubProgramRepository;
        private readonly IBrugerProgramRepository _brugerProgramRepository;

        public ProgramPlanService(
            IProgramPlanRepository programPlanRepository,
            ITræningRepository træningRepository,
            IKlubProgramRepository klubProgramRepository,
            IBrugerProgramRepository brugerProgramRepository)
        {
            _programPlanRepository = programPlanRepository ?? throw new ArgumentNullException(nameof(programPlanRepository));
            _træningRepository = træningRepository ?? throw new ArgumentNullException(nameof(træningRepository));
            _klubProgramRepository = klubProgramRepository ?? throw new ArgumentNullException(nameof(klubProgramRepository));
            _brugerProgramRepository = brugerProgramRepository ?? throw new ArgumentNullException(nameof(brugerProgramRepository));
        }
        // Get all ProgramPlans as DTOs
        public async Task<List<ProgramPlanDTO>> GetAllProgramPlansAsync()
        {
            var programPlans = await _programPlanRepository.GetAllAsync();
            return programPlans.Select(pp => new ProgramPlanDTO
            {
                ProgramID = pp.ProgramID,
                ProgramNavn = pp.ProgramNavn,
                OprettelseDato = pp.OprettelseDato,
                Længde = pp.Længde,
                Beskrivelse = pp.Beskrivelse
            }).ToList();
        }

        // Get ProgramPlan by ID
        public async Task<ProgramPlanDTO?> GetProgramPlanByIdAsync(int id)
        {
            var programPlan = await _programPlanRepository.GetByIdAsync(id);
            if (programPlan == null) return null;

            return new ProgramPlanDTO
            {
                ProgramID = programPlan.ProgramID,
                ProgramNavn = programPlan.ProgramNavn,
                OprettelseDato = programPlan.OprettelseDato,
                Længde = programPlan.Længde,
                Beskrivelse = programPlan.Beskrivelse
            };
        }

        //public async Task<ProgramPlanDTO?> CreateProgramPlanAsync(ProgramPlanDTO programPlanDto)
        //{
        //    // Validate required fields
        //    if (string.IsNullOrEmpty(programPlanDto.ProgramNavn))
        //    {
        //        return null; // Return null or a custom error DTO if needed
        //    }

        //    if (string.IsNullOrEmpty(programPlanDto.Beskrivelse))
        //    {
        //        return null; // Return null or a custom error DTO if needed
        //    }

        //    // Validate Længde field (should be positive)
        //    if (programPlanDto.Længde <= 0)
        //    {
        //        return null; // Return null or a custom error DTO if needed
        //    }

        //    // Validate OprettelseDato (should be a valid date in the past or present)
        //    if (programPlanDto.OprettelseDato > DateTime.Now)
        //    {
        //        return null; // Return null or a custom error DTO if needed
        //    }

        //    // Create the ProgramPlan entity based on the validated DTO
        //    var newProgramPlan = new ProgramPlan
        //    {
        //        ProgramNavn = programPlanDto.ProgramNavn,
        //        OprettelseDato = programPlanDto.OprettelseDato,
        //        Længde = programPlanDto.Længde,
        //        Beskrivelse = programPlanDto.Beskrivelse
        //    };

        //    // Save the new ProgramPlan to the repository
        //    var createdProgramPlan = await _programPlanRepository.CreateAsync(newProgramPlan);

        //    // Return the created ProgramPlan as DTO
        //    return new ProgramPlanDTO
        //    {
        //        ProgramID = createdProgramPlan.ProgramID,
        //        ProgramNavn = createdProgramPlan.ProgramNavn,
        //        OprettelseDato = createdProgramPlan.OprettelseDato,
        //        Længde = createdProgramPlan.Længde,
        //        Beskrivelse = createdProgramPlan.Beskrivelse
        //    };
        //}
        public async Task<ProgramPlanDTO> CreateProgramPlanWithBrugerAndKlubAsync(ProgramPlanDTO dto)
        {
            // Create ProgramPlan object
            var programPlan = new ProgramPlan
            {
                ProgramNavn = dto.ProgramNavn,
                OprettelseDato = dto.OprettelseDato,
                Længde = dto.Længde,
                Beskrivelse = dto.Beskrivelse
            };

            // Save ProgramPlan first to get ProgramID
            var createdProgramPlan = await _programPlanRepository.CreateAsync(programPlan);

            // Validate BrugerID (User ID)

            

            if (dto.BrugerID != null && dto.BrugerID != 0)
            {
                var brugerProgram = new BrugerProgram
                {
                    BrugerID = dto.BrugerID,
                    ProgramID = createdProgramPlan.ProgramID,
                };
                // Save the 
                await _brugerProgramRepository.CreateBrugerProgramAsync(brugerProgram);
            }

            //var klub = await _klubService.GetKlubByIdAsync(dto.KlubID);

            // Validate KlubID (Club ID)
            if (dto.KlubID != null && dto.KlubID != 0)
            {
                var klubprogram = new KlubProgram
                {
                    KlubID = dto.KlubID,
                    ProgramID = createdProgramPlan.ProgramID,
                };
                // Save the 
                await _klubProgramRepository.CreateKlubProgramAsync(klubprogram);
            }


            foreach (var træninger in dto.Træninger)
            {
                var træning = new Træning
                {
                    ProgramID = createdProgramPlan.ProgramID,
                    TeoriID = træninger.TeoriID,
                    TeknikID = træninger.TeknikID,
                    ØvelseID = træninger.ØvelseID,
                    QuizID = træninger.QuizID,
                    Tid = træninger.Tid,
                    TræningRækkefølge = træninger.TræningRækkefølge // Increment the sequence for each training session
                };

                // Save the Træning record to the database
                await _træningRepository.CreateTræningAsync(træning);
            }

            return dto;
        }
        public async Task<ProgramPlanDTO> UpdateProgramPlanWithBrugerAndKlubAsync(int id, ProgramPlanDTO dto)
        {
            var existingProgramPlan = await _programPlanRepository.GetByIdAsync(id);
            if (existingProgramPlan == null)
                return null; // Handle not found case

            // Update ProgramPlan fields
            existingProgramPlan.ProgramNavn = dto.ProgramNavn;
            existingProgramPlan.OprettelseDato = dto.OprettelseDato;
            existingProgramPlan.Længde = dto.Længde;
            existingProgramPlan.Beskrivelse = dto.Beskrivelse;

            await _programPlanRepository.UpdateAsync(existingProgramPlan);

            //// Update Bruger association
            //if (dto.BrugerID != null && dto.BrugerID != 0)
            //{
            //    var existingBrugerProgram = await _brugerProgramRepository.GetBrugerProgramByIdAsync(BrugerID, programPlanId);
            //    if (existingBrugerProgram == null)
            //    {
            //        await _brugerProgramRepository.CreateBrugerProgramAsync(new BrugerProgram
            //        {
            //            BrugerID = dto.BrugerID,
            //            ProgramID = programPlanId
            //        });
            //    }
            //    else
            //    {
            //        existingBrugerProgram.BrugerID = dto.BrugerID;
            //        await _brugerProgramRepository.(existingBrugerProgram);
            //    }
            //}

            //// Update Klub association
            //if (dto.KlubID != null && dto.KlubID != 0)
            //{
            //    var existingKlubProgram = await _klubProgramRepository.GetByProgramIdAsync(programPlanId);
            //    if (existingKlubProgram == null)
            //    {
            //        await _klubProgramRepository.CreateKlubProgramAsync(new KlubProgram
            //        {
            //            KlubID = dto.KlubID,
            //            ProgramID = programPlanId
            //        });
            //    }
            //    else
            //    {
            //        existingKlubProgram.KlubID = dto.KlubID;
            //        await _klubProgramRepository.UpdateKlubProgramAsync(existingKlubProgram);
            //    }
            //}

            // Update Træninger
            //await _træningRepository.DeleteByProgramIdAsync(programPlanId); // Remove old træninger
            //foreach (var træningDTO in dto.Træninger)
            //{
            //    await _træningRepository.CreateTræningAsync(new Træning
            //    {
            //        ProgramID = programPlanId,
            //        TeoriID = træningDTO.TeoriID,
            //        TeknikID = træningDTO.TeknikID,
            //        ØvelseID = træningDTO.ØvelseID,
            //        QuizID = træningDTO.QuizID,
            //        Tid = træningDTO.Tid,
            //        TræningRækkefølge = træningDTO.TræningRækkefølge
            //    });
            //}
            var existingTræninger = await _træningRepository.GetByProgramIdAsync(id);

            // Convert existing træninger to a dictionary for easy lookup
            var træningDictionary = existingTræninger.ToDictionary(t => t.TræningID);

            // Loop through new træninger
            foreach (var træningDTO in dto.Træninger)
            {
                if (træningDictionary.TryGetValue(træningDTO.TræningID, out var existingTræning))
                {
                    // Update existing træning
                    existingTræning.TeoriID = træningDTO.TeoriID;
                    existingTræning.TeknikID = træningDTO.TeknikID;
                    existingTræning.ØvelseID = træningDTO.ØvelseID;
                    existingTræning.QuizID = træningDTO.QuizID;
                    existingTræning.Tid = træningDTO.Tid;
                    existingTræning.TræningRækkefølge = træningDTO.TræningRækkefølge;

                    await _træningRepository.UpdateTræningAsync(existingTræning);
                }
                else
                {
                    // Insert new træning
                    await _træningRepository.CreateTræningAsync(new Træning
                    {
                        ProgramID = id,
                        TeoriID = træningDTO.TeoriID,
                        TeknikID = træningDTO.TeknikID,
                        ØvelseID = træningDTO.ØvelseID,
                        QuizID = træningDTO.QuizID,
                        Tid = træningDTO.Tid,
                        TræningRækkefølge = træningDTO.TræningRækkefølge
                    });
                }
            }

            // Remove træninger that are not in the new list
            var newTræningIds = dto.Træninger.Select(t => t.TræningID).ToHashSet();
            foreach (var existing in existingTræninger)
            {
                if (!newTræningIds.Contains(existing.TræningID))
                {
                    await _træningRepository.DeleteTræningAsync(existing.TræningID);
                }
            }

            return dto;
        }

        // Update ProgramPlan based on DTO
        public async Task<bool> UpdateProgramPlanAsync(int id, ProgramPlanDTO programPlanDto)
        {
            if (id != programPlanDto.ProgramID) return false;

            var existingProgramPlan = await _programPlanRepository.GetByIdAsync(id);
            if (existingProgramPlan == null) return false;

            existingProgramPlan.ProgramNavn = programPlanDto.ProgramNavn;
            existingProgramPlan.OprettelseDato = programPlanDto.OprettelseDato;
            existingProgramPlan.Længde = programPlanDto.Længde;
            existingProgramPlan.Beskrivelse = programPlanDto.Beskrivelse;

            return await _programPlanRepository.UpdateAsync(existingProgramPlan);
        }

        // Delete ProgramPlan by ID
        public async Task<bool> DeleteProgramPlanAsync(int id)
        {
            return await _programPlanRepository.DeleteAsync(id);
        }

        // Get all programs with their training, quiz, teori, teknik, and øvelse
        public async Task<List<ProgramPlanDTO>> GetAllProgramsAsync()
        {
            var programs = await _programPlanRepository.GetAllAsync();

            return programs.Select(p => new ProgramPlanDTO
            {
                ProgramID = p.ProgramID,
                ProgramNavn = p.ProgramNavn,
                Træninger = p.Træninger.Select(t => new TræningDTO
                {
                    TræningID = t.TræningID,
                    Quiz = t.Quiz != null ? new QuizDTO
                    {
                        QuizID = t.Quiz.QuizID,
                        QuizNavn = t.Quiz.QuizNavn
                    } : null,
                    Teori = t.Teori != null ? new TeoriDTO
                    {
                        TeoriID = t.Teori.TeoriID,
                        TeoriNavn = t.Teori.TeoriNavn
                    } : null,
                    Teknik = t.Teknik != null ? new TeknikDTO
                    {
                        TeknikID = t.Teknik.TeknikID,
                        TeknikNavn = t.Teknik.TeknikNavn
                    } : null,
                    Øvelse = t.Øvelse != null ? new ØvelseDTO
                    {
                        ØvelseID = t.Øvelse.ØvelseID,
                        ØvelseNavn = t.Øvelse.ØvelseNavn
                    } : null
                }).ToList()
            }).ToList();
        }

        public async Task<ProgramPlanDTO?> GetProgramByIdAsync(int id)
        {
            var program = await _programPlanRepository.GetByIdAsync(id);
            if (program == null) return null;

            return new ProgramPlanDTO
            {
                ProgramID = program.ProgramID,
                ProgramNavn = program.ProgramNavn,
                Træninger = program.Træninger.Select(t => new TræningDTO
                {
                    TræningID = t.TræningID,
                    Quiz = t.Quiz != null ? new QuizDTO
                    {
                        QuizID = t.Quiz.QuizID,
                        QuizNavn = t.Quiz.QuizNavn
                    } : null,
                    Teori = t.Teori != null ? new TeoriDTO
                    {
                        TeoriID = t.Teori.TeoriID,
                        TeoriNavn = t.Teori.TeoriNavn
                    } : null,
                    Teknik = t.Teknik != null ? new TeknikDTO
                    {
                        TeknikID = t.Teknik.TeknikID,
                        TeknikNavn = t.Teknik.TeknikNavn
                    } : null,
                    Øvelse = t.Øvelse != null ? new ØvelseDTO
                    {
                        ØvelseID = t.Øvelse.ØvelseID,
                        ØvelseNavn = t.Øvelse.ØvelseNavn
                    } : null
                }).ToList()
            };
        }

        public async Task<List<ProgramPlanDTO>> GetProgramsByBrugerAsync(int brugerId)
        {
            var programs = await _programPlanRepository.GetAllByBrugerIdAsync(brugerId);

            return programs.Select(p => new ProgramPlanDTO
            {
                ProgramID = p.ProgramID,
                ProgramNavn = p.ProgramNavn,
                Træninger = p.Træninger.Select(t => new TræningDTO
                {
                    TræningID = t.TræningID,
                    Quiz = t.Quiz != null ? new QuizDTO
                    {
                        QuizID = t.Quiz.QuizID,
                        QuizNavn = t.Quiz.QuizNavn
                    } : null,
                    Teori = t.Teori != null ? new TeoriDTO
                    {
                        TeoriID = t.Teori.TeoriID,
                        TeoriNavn = t.Teori.TeoriNavn
                    } : null,
                    Teknik = t.Teknik != null ? new TeknikDTO
                    {
                        TeknikID = t.Teknik.TeknikID,
                        TeknikNavn = t.Teknik.TeknikNavn
                    } : null,
                    Øvelse = t.Øvelse != null ? new ØvelseDTO
                    {
                        ØvelseID = t.Øvelse.ØvelseID,
                        ØvelseNavn = t.Øvelse.ØvelseNavn
                    } : null
                }).ToList()
            }).ToList();
        }


        public async Task<List<ProgramPlanDTO>> GetProgramsByKlubAsync(int klubId)
        {
            var programs = await _programPlanRepository.GetAllByKlubIdAsync(klubId);

            return programs.Select(p => new ProgramPlanDTO
            {
                ProgramID = p.ProgramID,
                ProgramNavn = p.ProgramNavn,
                Træninger = p.Træninger.Select(t => new TræningDTO
                {
                    TræningID = t.TræningID,
                    Quiz = t.Quiz != null ? new QuizDTO
                    {
                        QuizID = t.Quiz.QuizID,
                        QuizNavn = t.Quiz.QuizNavn
                    } : null,
                    Teori = t.Teori != null ? new TeoriDTO
                    {
                        TeoriID = t.Teori.TeoriID,
                        TeoriNavn = t.Teori.TeoriNavn
                    } : null,
                    Teknik = t.Teknik != null ? new TeknikDTO
                    {
                        TeknikID = t.Teknik.TeknikID,
                        TeknikNavn = t.Teknik.TeknikNavn
                    } : null,
                    Øvelse = t.Øvelse != null ? new ØvelseDTO
                    {
                        ØvelseID = t.Øvelse.ØvelseID,
                        ØvelseNavn = t.Øvelse.ØvelseNavn
                    } : null
                }).ToList()
            }).ToList();
        }

    }
}
