using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SachController : ControllerBase
    {
        //gan gia tri cho sach
        public static List<Sach> sach = new List<Sach>
            {
                new Sach {
                    Id= 1,
                    Ten="dac nhan tam",
                    TheLoai="self help",
                    TacGia="thich nhat hanh"},
                new Sach {
                    Id= 2,
                    Ten="dac nhan tam2",
                    TheLoai="self help2",
                    TacGia="thich nhat hanh2"}
        };
        // tao bien chi dung de doc (read only)
        private readonly DataContext context;
        public SachController(DataContext context)
        {
            this.context = context;
        }
        //tao phuong thuc get
        [HttpGet]
        public async Task<ActionResult<List<Sach>>> Get()
        {
            return Ok(await context.sach.ToListAsync());
        }

        //tao phuong thuc post them sach 
        [HttpPost]
        public async Task<ActionResult<List<Sach>>> AddSach(Sach sachPost)
        {
            context.sach.Add(sachPost);
            await context.SaveChangesAsync();
            return Ok(await context.sach.ToListAsync());
        }

        //tao phuong thuc Get 1 cuon sach
        [HttpGet("{id}")]
        public async Task<ActionResult<Sach>> Get(int id)
        {
            var Sach= await context.sach.FindAsync(id);
            if (Sach == null)
                return BadRequest("Sach not found");
            return Ok(Sach);
        }

        //tao phuong thuc sua sach
        [HttpPut]
        public async Task<ActionResult<List<Sach>>> EditSach(Sach request)
        {
              var dbSach= await context.sach.FindAsync(request.Id);
            if (dbSach == null)
                return BadRequest("K co sach de sua");

            dbSach.Id = request.Id;
            dbSach.TheLoai = request.TheLoai; 
            dbSach.TacGia = request.TacGia;
            dbSach.Ten = request.Ten;
            await context.SaveChangesAsync();

            return Ok(dbSach);
        }
        //tao phuong thuc xoa 1 cuon sach
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Sach>>> Delete(int id)
        {
            var Sach = await context.sach.FindAsync(id);
            if (Sach == null)
                return BadRequest("Sach not found");
            context.sach.Remove(Sach);
            await context.SaveChangesAsync();
            return Ok(await context.sach.ToListAsync());
        }
    }
}
