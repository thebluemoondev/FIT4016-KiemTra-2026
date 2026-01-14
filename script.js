// Cấu hình URL API
const BASE_URL = "https://d1snf2t2-5095.asse.devtunnels.ms";
const STUDENT_API = `${BASE_URL}/api/students`;

// Dữ liệu Schools Hardcoded
const SCHOOLS_DATA = [
    { id: 1, name: "FPT Polytechnic" },
    { id: 2, name: "Vinschool" },
    { id: 3, name: "British Vietnamese International School" },
    { id: 4, name: "Hanoi - Amsterdam" },
    { id: 5, name: "Le Hong Phong High School" },
    { id: 6, name: "Doan Thi Diem Primary School" },
    { id: 7, name: "Marie Curie School" },
    { id: 8, name: "Nguyen Sieu School" },
    { id: 9, name: "Lomonoxop School" },
    { id: 10, name: "VAS - Vietnam Australia School" }
];

// --- BIẾN QUẢN LÝ PHÂN TRANG ---
let allStudents = []; // Lưu trữ toàn bộ dữ liệu từ API
let currentPage = 1;
const rowsPerPage = 10;

document.addEventListener("DOMContentLoaded", () => {
    loadStudents();
    loadSchoolsToDropdown();

    document.getElementById("student-form").addEventListener("submit", handleFormSubmit);

    window.onclick = (event) => {
        const modal = document.getElementById("studentModal");
        if (event.target == modal) closeModal();
    };
});

// 1. Tải danh sách học sinh (Lấy toàn bộ về trước)
async function loadStudents() {
    try {
        const response = await fetch(STUDENT_API);
        if (!response.ok) throw new Error("Cannot fetch students");

        allStudents = await response.json();
        // Sau khi lấy dữ liệu, quay về trang 1 và render
        currentPage = 1;
        renderTable();
    } catch (error) {
        console.error("Error:", error);
        alert("Error: Could not load student list.");
    }
}

// 2. Hàm Render bảng theo trang (Hiển thị 10 dòng)
function renderTable() {
    const tbody = document.getElementById("student-list");
    tbody.innerHTML = "";

    // Tính toán chỉ số bắt đầu và kết thúc
    const start = (currentPage - 1) * rowsPerPage;
    const end = start + rowsPerPage;
    const paginatedItems = allStudents.slice(start, end);

    paginatedItems.forEach(s => {
        const schoolObj = SCHOOLS_DATA.find(sch => sch.id == s.schoolId);
        const displaySchoolName = s.schoolName || (schoolObj ? schoolObj.name : "N/A");

        const tr = document.createElement("tr");
        tr.innerHTML = `
            <td>${s.fullName}</td>
            <td>${s.studentId}</td>
            <td>${s.email}</td>
            <td>${displaySchoolName}</td>
            <td style="text-align:center">
                <button class="btn-view" onclick='viewDetails(${JSON.stringify(s)})'>View</button>
                <button class="btn-edit" onclick='editStudent(${JSON.stringify(s)})'>Edit</button>
                <button class="btn-delete" onclick="deleteStudent(${s.id})">Delete</button>
            </td>
        `;
        tbody.appendChild(tr);
    });

    renderPaginationControls();
}

// 3. Hàm tạo nút bấm chuyển trang
function renderPaginationControls() {
    const totalPages = Math.ceil(allStudents.length / rowsPerPage);
    const container = document.getElementById("pagination-container");
    // Lưu ý: Bạn cần thêm <div id="pagination-container"></div> vào HTML dưới table

    if (!container) return;
    container.innerHTML = "";

    for (let i = 1; i <= totalPages; i++) {
        const btn = document.createElement("button");
        btn.innerText = i;
        btn.className = (i === currentPage) ? "btn-page active" : "btn-page";
        btn.style.margin = "0 2px";
        btn.onclick = () => {
            currentPage = i;
            renderTable();
        };
        container.appendChild(btn);
    }
}

// 4. Đổ danh sách trường vào Select Option
function loadSchoolsToDropdown() {
    const select = document.getElementById("school-id");
    select.innerHTML = '<option value="">-- Select School --</option>';

    SCHOOLS_DATA.forEach(school => {
        const opt = document.createElement("option");
        opt.value = school.id;
        opt.textContent = school.name;
        select.appendChild(opt);
    });
}

// 5. Xử lý Thêm/Sửa
async function handleFormSubmit(e) {
    e.preventDefault();

    const id = document.getElementById("student-id-pk").value;
    const studentData = {
        fullName: document.getElementById("full-name").value,
        studentId: document.getElementById("student-id-code").value,
        email: document.getElementById("email").value,
        phone: document.getElementById("phone").value || null,
        schoolId: parseInt(document.getElementById("school-id").value)
    };

    const isUpdate = id !== "";
    const url = isUpdate ? `${STUDENT_API}/${id}` : STUDENT_API;
    const method = isUpdate ? "PUT" : "POST";

    try {
        const response = await fetch(url, {
            method: method,
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(studentData)
        });

        if (response.ok) {
            alert(isUpdate ? "Student updated!" : "Student created!");
            closeModal();
            loadStudents(); // Tải lại dữ liệu mới
        } else {
            const errData = await response.json();
            alert("Error: " + (errData.message || "Invalid data."));
        }
    } catch (error) {
        alert("System error.");
    }
}

// 6. Xóa học sinh
async function deleteStudent(id) {
    if (confirm("Are you sure?")) {
        try {
            const response = await fetch(`${STUDENT_API}/${id}`, { method: "DELETE" });
            if (response.ok) {
                alert("Deleted!");
                loadStudents();
            }
        } catch (error) {
            alert("Error deleting.");
        }
    }
}

// --- ĐIỀU KHIỂN GIAO DIỆN MODAL ---
function openModal() {
    const modal = document.getElementById("studentModal");
    const btnSave = document.getElementById("btn-save");
    const form = document.getElementById("student-form");

    modal.style.display = "block";
    form.reset();
    document.getElementById("student-id-pk").value = "";
    Array.from(form.elements).forEach(el => el.disabled = false);
    btnSave.style.display = "block";
}

function closeModal() {
    document.getElementById("studentModal").style.display = "none";
}

function editStudent(student) {
    openModal();
    document.getElementById("modal-title").innerText = "Update Student Information";
    document.getElementById("student-id-pk").value = student.id;
    document.getElementById("full-name").value = student.fullName;
    document.getElementById("student-id-code").value = student.studentId;
    document.getElementById("email").value = student.email;
    document.getElementById("phone").value = student.phone || "";
    document.getElementById("school-id").value = student.schoolId;
}

function viewDetails(student) {
    editStudent(student);
    document.getElementById("modal-title").innerText = "View Student Details";
    const form = document.getElementById("student-form");
    Array.from(form.elements).forEach(el => el.disabled = true);
    document.getElementById("btn-save").style.display = "none";
}