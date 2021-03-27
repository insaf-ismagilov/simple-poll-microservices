DROP FUNCTION IF EXISTS public.polls_create;

CREATE OR REPLACE FUNCTION public.polls_create(
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
    v_new_poll_id int;
BEGIN
    INSERT INTO public.polls (title, status, type)
    VALUES (p_title, p_status, p_type)
    RETURNING id INTO v_new_poll_id;

    INSERT
    INTO public.poll_options (text, value, poll_id)
    SELECT o.text, o.value, v_new_poll_id
    FROM jsonb_to_recordset(p_options) AS o (text text,
                                             value text);

    RETURN v_new_poll_id;
END;
$$