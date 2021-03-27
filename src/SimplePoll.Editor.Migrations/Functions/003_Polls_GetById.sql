DROP FUNCTION IF EXISTS public.polls_get_by_id;

CREATE OR REPLACE FUNCTION public.polls_get_by_id(p_id int)
    RETURNS TABLE
            (
                id                  int,
                title               text,
                status              int,
                type                int,
                poll_option_id      int,
                poll_option_text    text,
                poll_option_value   text,
                poll_option_poll_id int
            )
    LANGUAGE sql
AS
$$
SELECT p.id,
       p.title,
       p.status,
       p.type,
       po.id      AS poll_option_id,
       po.text    AS poll_option_text,
       po.value   AS poll_option_value,
       po.poll_id AS poll_option_poll_id
FROM public.polls p
         LEFT JOIN public.poll_options po ON po.poll_id = p.id
WHERE p.id = p_id
$$