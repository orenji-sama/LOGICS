-- Start of DDL Script for Package Body ASGUF2.PERMISSIONS_PKG
-- Generated 31.01.2018 10:52:17 from ASGUF2@sequent2.MLC.GOV

CREATE OR REPLACE 
PACKAGE BODY permissions_pkg
IS
--
-- To modify this template, edit file PKGBODY.TXT in TEMPLATE
-- directory of SQL Navigator
--
-- Purpose: Briefly explain the functionality of the package body
--
-- MODIFICATION HISTORY
-- Person      Date    Comments
-- ---------   ------  ------------------------------------------
   -- Enter procedure, function bodies as shown below

    FUNCTION CheckGroup
      ( ActLogin            IN VARCHAR2,
        GroupName           IN VARCHAR2 )
    RETURN VARCHAR2
    IS
        RESULT NUMBER;
    BEGIN
        /*IF INSTR(GroupName,'Регистрация') != 0 THEN

            select count(*) into RESULT from
            reon_adm.usergroups@REONADM.MLC.GOV u, reon_adm.useroperrights@REONADM.MLC.GOV r, reon_adm.actions@REONADM.MLC.GOV a
            where
            UPPER(SUBSTR(u.userwinnt,INSTR(u.userwinnt,'\')+1)) = UPPER(ActLogin)
            and u.groupid = r.groupid
            and r.actid = a.actid
            and UPPER(a.name) like UPPER(GroupName||'%');
        ELSE*/
            select count(*) into RESULT from
            reon_adm.usergroups@REONADM.MLC.GOV u, reon_adm.useroperrights@REONADM.MLC.GOV r, reon_adm.actions@REONADM.MLC.GOV a
            where
            UPPER(SUBSTR(u.userwinnt,INSTR(u.userwinnt,'\')+1)) = UPPER(ActLogin)
            and u.groupid = r.groupid
            and r.actid = a.actid
            and a.projid = 4
            and UPPER(a.name) = UPPER(GroupName);

            IF (RESULT = 0 and INSTR(GroupName,'Регистрация') != 0 and INSTR(GroupName,'Регистрация всех документов') = 0) THEN
            select count(*) into RESULT from
            reon_adm.usergroups@REONADM.MLC.GOV u, reon_adm.useroperrights@REONADM.MLC.GOV r, reon_adm.actions@REONADM.MLC.GOV a
            where
            UPPER(SUBSTR(u.userwinnt,INSTR(u.userwinnt,'\')+1)) = UPPER(ActLogin)
            and u.groupid = r.groupid
            and r.actid = a.actid
            and a.projid = 4
            and UPPER(a.name) like UPPER('Регистрация%');
            END IF;

        --END IF;

       RETURN RESULT;
    EXCEPTION
       WHEN OTHERS THEN
         RETURN 0;
    END;
-------------------------------------------------------------------------------
    FUNCTION CheckGroup
      ( ActLogin            IN VARCHAR2 )
    RETURN VARCHAR2
    IS
        RESULT NUMBER;
    BEGIN
        select count(*) into RESULT from
        reon_adm.usergroups@REONADM.MLC.GOV u, reon_adm.useroperrights@REONADM.MLC.GOV r, reon_adm.actions@REONADM.MLC.GOV a
        where
        UPPER(SUBSTR(u.userwinnt,INSTR(u.userwinnt,'\')+1)) = UPPER(ActLogin)
        and u.groupid = r.groupid
        and r.actid = a.actid
        and a.projid = 4
        and a.name not in ('Действия от имени Заявителя', 'Действия от имени Департамента');

       RETURN RESULT;
    EXCEPTION
       WHEN OTHERS THEN
         RETURN 0;
    END;
-------------------------------------------------------------------------------
    FUNCTION GetLoginByFullName
      ( FullName           IN VARCHAR2 )
    RETURN VARCHAR2
    IS Result VARCHAR2(30);
    BEGIN
       SELECT dm.users.username
       INTO Result
       FROM dm.users
       WHERE dm.users.fam = FullName;
       RETURN Result;
    EXCEPTION
       WHEN OTHERS THEN
         RETURN '';
    END;
-------------------------------------------------------------------------------

   -- Enter further code below as specified in the Package spec.
END;
/

-- Grants for Package Body
GRANT EXECUTE ON permissions_pkg TO public
/


-- End of DDL Script for Package Body ASGUF2.PERMISSIONS_PKG

