CREATE INDEX idx_text ON Report (text);

DROP INDEX idx_text;

CREATE INDEX idx_title ON Report (title);

DROP INDEX idx_title;

CREATE INDEX idx_name ON Users (name);

DROP INDEX idx_name;

CREATE INDEX idx_comment_text ON Comment (text);
