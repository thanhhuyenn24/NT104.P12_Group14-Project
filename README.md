### [Repository Assignment](https://github.com/thanhhuyenn24/NT106.P12_Group14_Assignment)

# Game: Tam sao thất bản (Catch the Word!)
## Giới thiệu
- Trò chơi trực tuyến ngày càng trở nên phổ biến nhờ sự phát triển của công nghệ và internet. Các tựa game đơn giản và thu hút nhiều lượt tham gia, tương tác cao, như trò chơi đoán từ: Pictionary, Gartic.io, Doodle Valley… không chỉ mang tính giải trí mà còn giúp kết nối bạn bè, gia đình, và người chơi trên khắp thế giới.
- **Catch the Word!** là trò chơi vẽ và đoán từ, nơi người chơi thay phiên nhau vẽ các từ khóa để người khác đoán. Với mỗi lượt, một người vẽ sẽ mô tả từ bằng hình ảnh, trong khi những người còn lại cố gắng đoán từ một cách nhanh nhất.
- Trò chơi này khuyến khích người chơi sáng tạo thông qua vẽ tranh, đồng thời thử thách khả năng suy đoán của những người tham gia. Đề tài "Tam sao thất bản" được chọn để hiện thực hóa ý tưởng xây dựng một nền tảng trò chơi sáng tạo, dễ sử dụng, không cần người chơi tải ứng dụng, và hỗ trợ kết nối nhiều người chơi.
- Đồ án của môn học **NT106**.
- **Giảng viên hướng dẫn**: Đặng Lê Bảo Chương.

## Thành viên
| MSSV       | Họ và Tên              |
|------------|------------------------|
| 23521466   | Đoàn Thanh Thảo        |
| 23520659   | Dương Thanh Huyền      |
| 23521038   | Đỗ Ngọc Thảo Nguyên    |

## Cách chơi
1. **Tạo phòng**: Người chơi đầu tiên (chủ phòng) sẽ chọn số lượng người tham gia, thời gian vẽ, và số vòng chơi.
   
2. **Bắt đầu chơi**: Người vẽ sẽ nhận từ để vẽ, giúp người đoán đoán đúng từ khóa đó.

3. **Đoán từ**: Người đoán nhập câu trả lời. Nếu đoán đúng, sẽ được thông báo người chơi đó đã đoán đúng, nếu đoán sai, hiện từ người chơi đó vừa đoán.

4. **Tính điểm**: Điểm được cộng cho người đoán đúng (+200) và người vẽ (+100), trò chơi tiếp tục lượt vẽ cho người khác đến khi hết số vòng đã cài đặt trước, người có nhiều điểm nhất sẽ thắng.

## Cách sử dụng
- Chạy server, chọn file từ (JSON) đã có sẵn trong dự án
- Chạy client

## Tính năng
-	Tạo và tham gia phòng chơi công khai (Cài đặt số người chơi, lượt chơi, thời lượng vẽ) 
-	Giao diện vẽ (bảng vẽ đơn giản, cấp quyền truy cập bảng vẽ cho mỗi lượt đoán và vẽ)
-	Hiển thị danh sách người dùng trong cùng một phòng 
-	Tính năng chat 
-	Điểm số và hiển thị người thắng cuộc
-	Gợi ý và hỗ trợ.
-	Đồng bộ nét vẽ khi người dùng mới tham gia vào phòng có sẵn - Xuất bản vẽ 
-	Vẽ, xóa, thu hồi nét vẽ

## Giao diện
![image](https://github.com/user-attachments/assets/35541457-83d7-45f9-92fa-27c8db749b1f)
<div align="center">
<h1>Căn giữa văn bản</h1>
</div>Hình 1: Giao diện người dùng khi tạo phòng
                     
![image](https://github.com/user-attachments/assets/2db13e77-0527-48be-8e16-fbeb22fafbdf)
                     Hình 2: Giao diện khi người dùng cài đặt
                     
![image](https://github.com/user-attachments/assets/db182345-4962-4222-9a2f-f5509a09ea34)
                     Hình 3: Giao diện bảng vẽ

## Mô hình phân rã chức năng
![image](https://github.com/user-attachments/assets/ce113fe1-d4b2-4a93-a467-8d5da353ab9c)

## DEMO
[Demo Catch the Word!](https://drive.google.com/file/d/1O4LcK-0G7ahz3BnV4a7E-qQeaD0xd_Sg/view?fbclid=IwZXh0bgNhZW0CMTEAAR0yhhujmkKaMlqOfpoV1D-LNCDidIL8GsopOAky4Uu1Y7Jny2BQ8AwFqGk_aem_Rqwgw0GPI19DKjnWtkRGOg)
