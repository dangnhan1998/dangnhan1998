namespace Hrms.Modules.Recruitment.Contracts;

/// <summary>
/// Cổng giao tiếp duy nhất để module khác gọi vào module Tuyển dụng.
/// Thêm phương thức tại đây khi có nhu cầu liên thông dữ liệu, không tham chiếu thẳng vào project Hrms.Modules.Recruitment.
/// </summary>
public interface IRecruitmentModuleApi;
