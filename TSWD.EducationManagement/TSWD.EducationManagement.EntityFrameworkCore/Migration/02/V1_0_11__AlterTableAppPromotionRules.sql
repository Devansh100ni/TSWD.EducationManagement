UPDATE AppPromotionRules
SET IsPercent =
    CASE
        WHEN IsPercent IN ('1', 'true', 'TRUE', 'yes', 'YES') THEN 1
        ELSE 0
    END;

ALTER TABLE AppPromotionRules
ALTER COLUMN [IsPercent] BIT NOT NULL;


ALTER TABLE AppPromotionRules
ADD CONSTRAINT DF_AppPromotionRules_IsPercent
DEFAULT (0) FOR [IsPercent];
