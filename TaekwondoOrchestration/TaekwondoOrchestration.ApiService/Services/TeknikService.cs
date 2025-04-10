﻿using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;


public class TeknikService
{
    private readonly ITeknikRepository _teknikRepository;

    public TeknikService(ITeknikRepository teknikRepository)
    {
        _teknikRepository = teknikRepository;
    }

    // Get all Tekniks as DTO
    public async Task<List<TeknikDTO>> GetAllTekniksAsync()
    {
        var teknikList = await _teknikRepository.GetAllTekniksAsync();
        return teknikList.Select(t => new TeknikDTO
        {
            TeknikID = t.TeknikID,
            TeknikNavn = t.TeknikNavn,
            TeknikBeskrivelse = t.TeknikBeskrivelse,
            TeknikBillede = t.TeknikBillede,
            TeknikVideo = t.TeknikVideo,
            TeknikLyd = t.TeknikLyd,
            PensumID = t.PensumID
        }).ToList();
    }

    // Get Teknik by ID
    public async Task<TeknikDTO?> GetTeknikByIdAsync(Guid id)
    {

        var teknik = await _teknikRepository.GetTeknikByIdAsync(id);
        if (teknik == null) return null;

        return MapToDto(teknik);
    }

    // Create Teknik
    public async Task<TeknikDTO> CreateTeknikAsync(TeknikDTO teknikDto)
    {
        if (teknikDto == null)
            return null;

        var newTeknik = new Teknik
        {
            TeknikNavn = teknikDto.TeknikNavn,
            TeknikBeskrivelse = teknikDto.TeknikBeskrivelse,
            TeknikBillede = teknikDto.TeknikBillede,
            TeknikVideo = teknikDto.TeknikVideo,
            TeknikLyd = teknikDto.TeknikLyd,
            PensumID = teknikDto.PensumID
        };

        var createdTeknik = await _teknikRepository.CreateTeknikAsync(newTeknik);

        return MapToDto(createdTeknik);
    }

    // Delete Teknik
    public async Task<bool> DeleteTeknikAsync(Guid id)
    {
        return await _teknikRepository.DeleteTeknikAsync(id);
    }

    // Update Teknik
    public async Task<bool> UpdateTeknikAsync(Guid id, TeknikDTO teknikDto)
    {

        var existingTeknik = await _teknikRepository.GetTeknikByIdAsync(id);
        if (existingTeknik == null) return false;

        var updatedTeknik = new Teknik
        {
            TeknikID = id,
            TeknikNavn = teknikDto.TeknikNavn,
            TeknikBeskrivelse = teknikDto.TeknikBeskrivelse,
            TeknikBillede = teknikDto.TeknikBillede,
            TeknikVideo = teknikDto.TeknikVideo,
            TeknikLyd = teknikDto.TeknikLyd,
            PensumID = teknikDto.PensumID
        };

        return await _teknikRepository.UpdateTeknikAsync(updatedTeknik);
    }

    // Get all Tekniks by Pensum ID
    public async Task<List<TeknikDTO>> GetAllTeknikByPensumAsync(Guid pensumId)
    {

        var teknikList = await _teknikRepository.GetTekniksByPensumAsync(pensumId);
        return teknikList.Select(t => MapToDto(t)).ToList();
    }

    // Get Teknik by TeknikNavn
    public async Task<TeknikDTO?> GetTeknikByTeknikNavnAsync(string teknikNavn)
    {
        if (string.IsNullOrWhiteSpace(teknikNavn)) return null;

        var teknik = await _teknikRepository.GetTeknikByTeknikNavnAsync(teknikNavn);
        if (teknik == null) return null;

        return MapToDto(teknik);
    }

    // Helper method for manual mapping
    private TeknikDTO MapToDto(Teknik teknik)
    {
        return new TeknikDTO
        {
            TeknikID = teknik.TeknikID,
            TeknikNavn = teknik.TeknikNavn,
            TeknikBeskrivelse = teknik.TeknikBeskrivelse,
            TeknikBillede = teknik.TeknikBillede,
            TeknikVideo = teknik.TeknikVideo,
            TeknikLyd = teknik.TeknikLyd,
            PensumID = teknik.PensumID
        };
    }
}
