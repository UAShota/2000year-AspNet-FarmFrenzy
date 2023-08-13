/* Formatted on 2010/10/11 09:39 (Formatter Plus v4.8.8) */
delete
--select *
    FROM arc
   WHERE id_state = 1
     AND (date_time BETWEEN :dtb AND :dte)
     AND (   (id_tag = 24584 AND id_int = 9)
          OR (id_tag = 24585 AND id_int = 9)
          OR (id_tag = 24586 AND id_int = 9)
          OR (id_tag = 24587 AND id_int = 9)
          OR (id_tag = 24588 AND id_int = 9)
          OR (id_tag = 24589 AND id_int = 9)
          OR (id_tag = 24590 AND id_int = 9)
          OR (id_tag = 24591 AND id_int = 9)
          OR (id_tag = 24592 AND id_int = 9)
          OR (id_tag = 24593 AND id_int = 9)
          OR (id_tag = 24594 AND id_int = 9)
         )
