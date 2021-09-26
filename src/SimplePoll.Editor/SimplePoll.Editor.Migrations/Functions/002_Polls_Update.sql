DROP FUNCTION IF EXISTS public.polls_update;

CREATE OR REPLACE FUNCTION public.polls_update(
    p_id int,
    p_title text,
    p_status int,
    p_type int,
    p_options jsonb
)
    RETURNS int
    LANGUAGE plpgsql
AS
$$
DECLARE
    v_updated_poll_id int;
BEGIN
    UPDATE public.polls p
    SET title              = p_title,
        status             = p_status,
        type               = p_type,
        last_modified_date = now() at time zone 'utc'
    WHERE p.id = p_id
    RETURNING id INTO v_updated_poll_id;

    WITH options AS (
        SELECT o.id, o.text, o.value
        FROM jsonb_to_recordset(p_options) AS o (
                                                 id int,
                                                 text text,
                                                 value text)
    ),
         to_delete_existing_options AS (
             SELECT po.id
             FROM public.poll_options po
             WHERE po.poll_id = v_updated_poll_id
               AND NOT EXISTS(SELECT 1 FROM options o WHERE o.id = po.id)
         ),
         delete_options AS (
             DELETE FROM public.poll_options po
                 WHERE po.id IN (SELECT id FROM to_delete_existing_options)
         ),
         update_options AS (
             UPDATE public.poll_options po
                 SET text = o.text,
                     value = o.value,
                     last_modified_date = now() at time zone 'utc'
                 FROM options o
                 WHERE po.id = o.id AND po.poll_id = v_updated_poll_id
         )
    INSERT
    INTO public.poll_options (text, value, poll_id)
    SELECT o.text, o.value, v_updated_poll_id
    FROM options o
    WHERE o.id IS NULL
       OR o.id = 0;

    RETURN v_updated_poll_id;
END;
$$