CREATE TRIGGER delete_materials
    ON Material
    FOR DELETE
AS
BEGIN
    DELETE FROM detailImportGoods
    WHERE id_material IN (SELECT IdMaterial FROM deleted)
END 
GO
ALTER TABLE UseMaterial 
ADD CONSTRAINT FK_UseMaterial_Material 
FOREIGN KEY (id_material) REFERENCES Material(IdMaterial);

BEGIN TRANSACTION
    DELETE FROM Material 
    WHERE IdMaterial =  'f75bcddc-6';
    
    DELETE FROM detailImportGoods 
    WHERE id_material =  'f75bcddc-6';
COMMIT TRANSACTION
--	sửa trigger
ALTER TRIGGER delete_materials
ON Material
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    DELETE FROM detailImportGoods
    WHERE id_material IN (SELECT IdMaterial FROM deleted)

	DELETE FROM UseMaterial
    WHERE id_material IN (SELECT IdMaterial FROM deleted)
END
Go



delete from Material where IdMaterial = '66363843-9'
    select * from Material where IdMaterial = '66363843-9'
	select * from UseMaterial WHERE id_material IN (SELECT IdMaterial FROM Material where IdMaterial = '66363843-9')
	select * from detailImportGoods WHERE id_material IN (SELECT IdMaterial FROM Material where IdMaterial = '66363843-9')
	
--drop trigger
drop trigger delete_staff 
drop trigger TGG_test_delete_materials
select * from Material
select * from UseMaterial
select * from detailImportGoods
SELECT * FROM sys.triggers


delete from staff where idStaff = '27564e9a-d'
select * from staff
select * from SelectedWorkShift
GO


CREATE TRIGGER trg_DeleteMaterial
ON Material
for DELETE
AS
BEGIN
    -- Xóa các bản ghi trong bảng UseMaterial liên quan đến các bản ghi đã xóa trong bảng Material
    DELETE FROM UseMaterial WHERE id_material IN (SELECT IdMaterial FROM deleted)
    -- Xóa các bản ghi trong bảng detailImportGoods liên quan đến các bản ghi đã xóa trong bảng Material
    DELETE FROM detailImportGoods WHERE id_material IN (SELECT IdMaterial FROM deleted)

    -- Xác định các bản ghi trong bảng UseMaterial đang sử dụng khóa ngoại trỏ đến các bản ghi đã xóa trong bảng Material
    DECLARE @id_material INT
    SELECT @id_material = IdMaterial FROM deleted
    IF EXISTS (SELECT * FROM UseMaterial WHERE id_material = @id_material)
    BEGIN
        RAISERROR ('This material is still being used in the UseMaterial table.', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END

    -- Xóa các bản ghi trong bảng Material
    DELETE FROM Material WHERE IdMaterial IN (SELECT IdMaterial FROM deleted)
END
GO

CREATE TRIGGER delete_staff
    ON staff
    AFTER DELETE
AS
BEGIN
    DELETE FROM SelectedWorkShift
    WHERE idStaff IN (SELECT idStaff FROM deleted)
END 
GO
delete from staff where idStaff = '2c164ce0-b'
select * from SelectedWorkShift where idStaff in (select idStaff from staff where idStaff = '2c164ce0-b')
select * from staff
select * from orders

select * from orders
select * from product

select * from order_detail
GO
CREATE TRIGGER delete_nv
    ON PhongBan
    INSTEAD OF DELETE
AS
BEGIN
	 DELETE FROM DuAN
    WHERE MaPB IN (SELECT MaPB FROM deleted)

    DELETE FROM NhanVien
    WHERE MaPB IN (SELECT MaPB FROM deleted)

	    DELETE FROM PhongBan
    WHERE MaPB IN (SELECT MaPB FROM deleted);
	
	
END 
GO

select * from PHONGBAN where MAPB = 'PB01'
select * from NhanVien where NhanVien.MAPB = 'PB01'
select * from DUAN where DUAN.MAPB = 'PB01'