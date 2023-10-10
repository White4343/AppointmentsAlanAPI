using AlanAPI.Context;
using AlanAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlanAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly DataContext _context;

    public PatientsController(DataContext context)
    {
        _context = context;
    }

    // GET: api/Patients
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
    {
        if (_context.Patients == null) return NotFound();
        return await _context.Patients.ToListAsync();
    }

    [HttpGet("ByUsers")]
    public async Task<ActionResult<IEnumerable<Patient>>> GetPatientsByUser()
    {
        var result = await (from patient in _context.Patients
            join user in _context.Users on patient.TelegramId equals user.TelegramId
            select new
            {
                Patient = patient,
                User = user
            }).ToListAsync();

        if (result == null) return NotFound();

        return Ok(result);
    }

    [HttpGet("MaxPatientId")]
    public async Task<ActionResult<int>> GetMaxPatientId()
    {
        var maxPatientId = await _context.Patients.MaxAsync(p => (int?)p.PatientId) ?? 0;
        return maxPatientId;
    }

    // GET: api/Patients/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Patient>> GetPatient(int id)
    {
        if (_context.Patients == null) return NotFound();
        var patient = await _context.Patients.FindAsync(id);

        if (patient == null) return NotFound();

        return patient;
    }

    // PUT: api/Patients/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPatient(int id, Patient patient)
    {
        if (id != patient.PatientId) return BadRequest();

        _context.Entry(patient).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PatientExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Patients
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Patient>> PostPatient(Patient patient)
    {
        if (_context.Patients == null) return Problem("Entity set 'DataContext.Patients'  is null.");

        if (patient == null) return BadRequest("Patient data is null.");

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPatient", new { id = patient.PatientId }, patient);
    }

    // DELETE: api/Patients/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        if (_context.Patients == null) return NotFound();
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null) return NotFound();

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PatientExists(int id)
    {
        return (_context.Patients?.Any(e => e.PatientId == id)).GetValueOrDefault();
    }
}