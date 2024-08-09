using StudentDetails.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace StudentDetails.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<Student> _students;

        public StudentService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _students = database.GetCollection<Student>("Student");
        }

        public async Task<List<Student>> GetAsync()
        {
            return await _students.Find(student => true).ToListAsync();
        }

        public async Task<Student> GetAsync(string id)
        {
            return await _students.Find<Student>(student => student.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Student student)
        {
            await _students.InsertOneAsync(student);
        }

        public async Task UpdateAsync(string id, Student student)
        {
            await _students.ReplaceOneAsync(student => student.Id == id, student);
        }

        public async Task RemoveAsync(string id)
        {
            await _students.DeleteOneAsync(student => student.Id == id);
        }
    }
}
