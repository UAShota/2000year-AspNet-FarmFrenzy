--delete
select *
    FROM arcs
   WHERE id_state = 1
     AND (date_time BETWEEN :dtb AND :dte)
     AND (   (id_tag = 24584)
          OR (id_tag = 24585)
          OR (id_tag = 24586)
          OR (id_tag = 24587)
          OR (id_tag = 24588)
          OR (id_tag = 24589)
          OR (id_tag = 24590)
          OR (id_tag = 24591)
          OR (id_tag = 24592)
          OR (id_tag = 24593)
          OR (id_tag = 24594)
         )