# HRMS Intimex

Phần mềm quản lý nhân sự mới của Tập đoàn Intimex, thay thế PMNS cũ (.NET 4.0 + SQL Server).
Gồm Web cho TCNS và cấp quản lý, App di động cho người lao động tự phục vụ.

Đây mới là **khung dự án**: đã có cấu trúc solution, cơ chế module, giao diện Web/App trống, test và CI.
Chưa có nghiệp vụ.

## Công nghệ

| Tầng | Lựa chọn |
|---|---|
| Backend | ASP.NET Core (.NET 10 LTS), EF Core, modular monolith |
| Cơ sở dữ liệu | SQL Server 2022+ (một database, mỗi module một schema) |
| Web | Blazor Web App, Interactive Server, MudBlazor |
| Mobile | .NET MAUI Blazor Hybrid, dùng chung giao diện với Web qua `Hrms.UI` |
| Xác thực | ASP.NET Core Identity (không SSO), cookie cho Web, token cho App |
| Log | Serilog |
| Test | xUnit v3 (Microsoft Testing Platform) |
| CI | GitHub Actions |

Chi tiết lý do chọn xem tài liệu "Đề xuất Tech Stack cho HRMS Intimex".

## Cấu trúc thư mục

```
Hrms.slnx                     Solution đầy đủ (gồm cả Mobile)
Hrms.Server.slnf              Solution filter không có Mobile, dùng khi máy chưa cài workload MAUI
src/
  Hrms.Api/                   Tiến trình chạy duy nhất: REST API /api/... + host giao diện Web
  Hrms.Web/                   Trang riêng của Web (App.razor, Routes, trang quản trị, trang tuyển dụng công khai)
  Hrms.UI/                    Razor Class Library dùng chung cho Web và App (layout, trang, component)
  Hrms.Mobile/                App .NET MAUI Blazor Hybrid (Android, iOS)
  Modules/
    Foundation/               Nền tảng: tổ chức, danh mục, tài khoản, phân quyền, phê duyệt, thông báo
    Recruitment/              Tuyển dụng
    Personnel/                Nhân sự
    Training/                 Đào tạo
    CnB/                      C&B: bảo hiểm, chấm công, nghỉ phép, tiền lương, Chatbot AI
  Shared/
    Hrms.SharedKernel/        Kiểu dữ liệu nền (Entity, IAuditable), không phụ thuộc framework
    Hrms.Infrastructure/      Cơ chế module (IModule), DbContext cơ sở, sau này: phê duyệt, văn bản, job nền
  Tools/
    Hrms.Migration/           Chuyển dữ liệu từ PMNS cũ và đối soát
tests/
  Hrms.UnitTests/             Unit test theo thư mục module
  Hrms.ArchitectureTests/     Kiểm tra ranh giới giữa các module
  Hrms.IntegrationTests/      Chạy Hrms.Api trong bộ nhớ và gọi HTTP
```

Mỗi module có hai project:

- `Hrms.Modules.X`: toàn bộ cài đặt (`Domain/`, `Application/`, `Infrastructure/`, `Endpoints/`), mặc định `internal`.
- `Hrms.Modules.X.Contracts`: interface và DTO công khai. Module khác chỉ được tham chiếu project này.

## Chuẩn bị máy

1. [.NET SDK 10](https://dotnet.microsoft.com/download/dotnet/10.0) (phiên bản ghi trong `global.json`).
2. SQL Server 2022 trở lên (Developer Edition cho máy dev). Chưa cần ngay vì khung chưa có bảng nào.
3. Visual Studio 2026 hoặc JetBrains Rider. Làm App thì cài thêm workload MAUI:
   ```
   dotnet workload install maui
   ```

## Chạy dự án

```bash
# Build và test phần server (không cần MAUI)
dotnet build Hrms.Server.slnf
dotnet test --solution Hrms.Server.slnf

# Chạy Web + API
dotnet run --project src/Hrms.Api --launch-profile https
```

Sau khi chạy:

- Web: https://localhost:7100
- Kiểm tra sống: https://localhost:7100/health
- Danh sách module: https://localhost:7100/api/modules
- OpenAPI (chỉ môi trường Development): https://localhost:7100/openapi/v1.json

Chạy App trên Android emulator:

```bash
dotnet build src/Hrms.Mobile -f net10.0-android -t:Run
```

Trên Windows có thể chạy App dạng cửa sổ (`-f net10.0-windows10.0.19041.0`) để thử nhanh giao diện.

### Chuỗi kết nối

Mặc định trong `src/Hrms.Api/appsettings.json` trỏ tới `localhost` với Windows Authentication.
Mỗi người đặt chuỗi kết nối riêng bằng user secrets, không sửa file chung:

```bash
dotnet user-secrets --project src/Hrms.Api set "ConnectionStrings:Hrms" "Server=...;Database=Hrms;..."
```

## Thêm một module mới

1. Tạo `src/Modules/<Ten>/Hrms.Modules.<Ten>` và `Hrms.Modules.<Ten>.Contracts` theo mẫu module có sẵn.
2. Cài đặt `IModule` (tên, schema, đăng ký service, map endpoint).
3. Khai báo module trong `src/Hrms.Api/Program.cs` và thêm project vào `Hrms.slnx`, `Hrms.Server.slnf`.
4. Thêm tên module vào `ModuleBoundaryTests` và `ModuleRegistrationTests`.

## Migration cơ sở dữ liệu

Mỗi module có `DbContext` và schema riêng, nên chạy lệnh EF theo từng context:

```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add Init --project src/Modules/Personnel/Hrms.Modules.Personnel --startup-project src/Hrms.Api --context PersonnelDbContext
dotnet ef database update --project src/Modules/Personnel/Hrms.Modules.Personnel --startup-project src/Hrms.Api --context PersonnelDbContext
```

## Lộ trình

1. Nền tảng: danh mục, sơ đồ tổ chức, tài khoản và phân quyền, engine phê duyệt, thông báo, migration PMNS cũ.
2. Nhân sự lõi: hồ sơ, HĐLĐ, bổ nhiệm, sinh văn bản, tìm kiếm, báo cáo.
3. C&B chấm công + App tự phục vụ.
4. C&B lương và bảo hiểm (chạy song song PMNS cũ ít nhất 2 kỳ lương).
5. Tuyển dụng.
6. Đào tạo.
7. AI (bóc tách CV, Chatbot) và dashboard tổng hợp.
