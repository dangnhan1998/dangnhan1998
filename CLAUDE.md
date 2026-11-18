# CLAUDE.md

Hướng dẫn cho Claude Code (và người mới) khi làm việc trong repo này.

## Dự án

HRMS Intimex: phần mềm quản lý nhân sự cho khoảng 1.500 CBNV, 100 người dùng đồng thời, chạy trên máy chủ nội bộ.
Đội phát triển 3 người, đều làm C#. Toàn bộ stack là C#: ASP.NET Core (.NET 10) + SQL Server,
Web Blazor (Interactive Server, MudBlazor), App .NET MAUI Blazor Hybrid.
Viết tài liệu, comment và thông báo cho người dùng bằng tiếng Việt; tên class, biến, route bằng tiếng Anh.

## Lệnh thường dùng

```bash
dotnet build Hrms.Server.slnf                 # build mọi thứ trừ Mobile
dotnet test --solution Hrms.Server.slnf       # chạy toàn bộ test (Microsoft Testing Platform)
dotnet run --project src/Hrms.Api             # chạy API + Web
dotnet build src/Hrms.Mobile -f net10.0-android   # cần workload maui-android
```

Máy Linux hoặc CI không có workload MAUI thì luôn dùng `Hrms.Server.slnf`, không dùng `Hrms.slnx`.

## Kiến trúc: modular monolith

- `Hrms.Api` là tiến trình duy nhất. Nó đăng ký các module trong `Program.cs`, phục vụ REST API cho App
  dưới `/api/{module-key}/...` và host giao diện Web.
- Module nghiệp vụ: `Foundation` (nền tảng), `Recruitment` (Tuyển dụng), `Personnel` (Nhân sự),
  `Training` (Đào tạo), `CnB` (C&B).
- Mỗi module có schema SQL Server riêng (`foundation`, `recruitment`, `personnel`, `training`, `cnb`)
  và `DbContext` riêng kế thừa `ModuleDbContext`.

### Quy tắc ranh giới (có test kiểm tra trong `Hrms.ArchitectureTests`)

1. Module chỉ được tham chiếu module khác qua project `*.Contracts`. Không tham chiếu thẳng project cài đặt.
2. Không truy vấn bảng của module khác. Cần dữ liệu thì gọi interface `I<Module>ModuleApi` trong Contracts.
3. Project `*.Contracts` chỉ chứa interface, DTO, enum; không chứa logic, không tham chiếu EF Core.
4. Kiểu trong project cài đặt mặc định để `internal`. Chỉ lớp `<Module>Module` là `public`.

### Vị trí đặt code

| Loại code | Đặt ở |
|---|---|
| Entity, value object, quy tắc nghiệp vụ | `Hrms.Modules.X/Domain` |
| Use case, xử lý lệnh/truy vấn | `Hrms.Modules.X/Application` |
| DbContext, cấu hình EF, tích hợp ngoài | `Hrms.Modules.X/Infrastructure` |
| Minimal API endpoint | `Hrms.Modules.X/Endpoints`, map trong `XModule.MapEndpoints` |
| Trang/component dùng cho cả Web và App | `Hrms.UI` |
| Trang chỉ có trên Web | `Hrms.Web` |
| Code chỉ có trên điện thoại (camera, QR, push, SecureStorage) | `Hrms.Mobile`, cung cấp qua interface để `Hrms.UI` gọi |
| Phê duyệt, sinh văn bản, thông báo, job nền dùng chung | `Hrms.Infrastructure` |

## Quy ước

- Tiền dùng `decimal`, không dùng `double`/`float`. Mọi công thức lương, BH, thuế, OT phải có unit test.
- Quy tắc nghiệp vụ có hiệu lực theo ngày (mức lương, tỷ lệ BH, giảm trừ gia cảnh) lưu kèm ngày hiệu lực, không hard-code.
- Ma trận phê duyệt cấu hình bằng dữ liệu, không hard-code theo loại đơn.
- Dữ liệu cá nhân và lương: kiểm tra quyền ở tầng truy vấn, ghi audit log khi xem/sửa (Nghị định 13/2023/NĐ-CP).
- Thời gian lưu `DateTimeOffset`; hiển thị theo giờ Việt Nam.
- Phiên bản gói NuGet khai báo tập trung trong `Directory.Packages.props`; trong csproj chỉ ghi tên gói.
- CI bật `TreatWarningsAsErrors`, nên sửa hết cảnh báo trước khi mở PR.
- Không commit chuỗi kết nối hay khóa API; dùng `dotnet user-secrets` khi dev.

## Tài liệu nghiệp vụ

Yêu cầu chi tiết nằm trong file "ĐỀ XUẤT PMNS MỚI.xlsx" (5 sheet: Tổng quan, Tuyển dụng, Nhân sự, Đào tạo, C&B)
và tài liệu phân tích nghiệp vụ của dự án. Đọc mục tương ứng trước khi làm một tính năng.
