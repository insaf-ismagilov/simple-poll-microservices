CREATE TABLE IF NOT EXISTS public.poll_options
(
    id      int GENERATED ALWAYS AS IDENTITY,
    text    text NOT NULL,
    value   text NOT NULL,
    poll_id int  NOT NULL,

    created_date       timestamp NOT NULL DEFAULT timezone('utc'::text, now()),
    last_modified_date timestamp NOT NULL DEFAULT timezone('utc'::text, now()),

    CONSTRAINT poll_options_pk PRIMARY KEY (id),
    CONSTRAINT poll_options_polls_fk FOREIGN KEY (poll_id) REFERENCES public.polls (id)
)