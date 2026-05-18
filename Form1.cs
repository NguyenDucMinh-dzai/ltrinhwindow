using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MusicBox_QuanLyThanhVien
{
    public partial class Form1 : Form
    {
        List<ThanhVien> dsThanhVien = new List<ThanhVien>();

        public Form1()
        {
            InitializeComponent();
        }

        // 1. Chức năng Thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            string sdt = txtSoDienThoai.Text.Trim();
            string ten = txtHoTen.Text.Trim();
            string diemStr = txtDiem.Text.Trim();

            if (string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra trùng số điện thoại
            if (dsThanhVien.Any(tv => tv.SDT == sdt))
            {
                MessageBox.Show("Số điện thoại này đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(diemStr, out int diem))
            {
                MessageBox.Show("Điểm phải là số nguyên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dsThanhVien.Add(new ThanhVien { SDT = sdt, HoTen = ten, Diem = diem });
            CapNhatBang(dsThanhVien);
            XoaTrongTextBox();

            MessageBox.Show("Thêm thành viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 2. Chức năng Tìm kiếm (Theo Số điện thoại)
        private void btnTim_Click(object sender, EventArgs e)
        {
            string sdtTim = txtSoDienThoai.Text.Trim();

            if (string.IsNullOrEmpty(sdtTim))
            {
                MessageBox.Show("Vui lòng nhập Số điện thoại vào ô trống để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tìm danh sách các thành viên có số điện thoại chứa chuỗi tìm kiếm
            var ketQua = dsThanhVien.Where(tv => tv.SDT.Contains(sdtTim)).ToList();

            if (ketQua.Count > 0)
            {
                CapNhatBang(ketQua); // Hiển thị kết quả tìm được lên DataGridView
            }
            else
            {
                MessageBox.Show("Không tìm thấy thành viên nào!", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CapNhatBang(dsThanhVien); // Reset lại bảng nếu không tìm thấy
            }
        }

        // 3. Chức năng Xóa (Theo Số điện thoại đang nhập hoặc chọn)
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sdtXoa = txtSoDienThoai.Text.Trim();

            if (string.IsNullOrEmpty(sdtXoa))
            {
                MessageBox.Show("Vui lòng nhập Số điện thoại của thành viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tìm thành viên chính xác theo SĐT
            ThanhVien tvCanXoa = dsThanhVien.FirstOrDefault(tv => tv.SDT == sdtXoa);

            if (tvCanXoa != null)
            {
                DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa thành viên {tvCanXoa.HoTen} không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    dsThanhVien.Remove(tvCanXoa);
                    CapNhatBang(dsThanhVien);
                    XoaTrongTextBox();
                    MessageBox.Show("Xóa thành viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy thành viên có số điện thoại này để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm cập nhật DataGridView (hỗ trợ truyền danh sách bất kỳ để phục vụ bộ lọc tìm kiếm)
        private void CapNhatBang(List<ThanhVien> danhSach)
        {
            dgvThanhVien.DataSource = null;
            dgvThanhVien.DataSource = danhSach;
        }

        // Hàm phụ xóa nhanh dữ liệu nhập trên form
        private void XoaTrongTextBox()
        {
            txtSoDienThoai.Clear();
            txtHoTen.Clear();
            txtDiem.Clear();
            txtSoDienThoai.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtSoDienThoai_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblSoDienThoai_Click(object sender, EventArgs e)
        {

        }
    }

    public class ThanhVien
    {
        public string SDT { get; set; }
        public string HoTen { get; set; }
        public int Diem { get; set; }
    }
}