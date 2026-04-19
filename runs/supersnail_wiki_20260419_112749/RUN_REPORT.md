# Super Snail Wiki Scrape - RUN REPORT

- **Total pages indexed:** 4125
- **Total pages fetched:** 4125
- **Total unique categories:** 562
- **Likely item/stat pages:** 3508 (Heuristic: 85% of pages appear to be game data)

## Top Categories
1. **Relics**: 2294
2. **Green Relics**: 731
3. **Orange Relics**: 728
4. **FAME Relics**: 465
5. **TECH Relics**: 427
6. **Purple Relics**: 418
7. **CIV Relics**: 398
8. **ART Relics**: 373
9. **Blue Relics**: 355
10. **ATK Boost**: 326

## Top Infobox/Template Types
- `CargoRelic`: 2088
- `CargoRelicResonance`: 1142
- `gearrow`: 422
- `RewardIconWithGearlink`: 215

## Extraction Quality (Estimated)
- **Item/Stat Extraction:** Very High. 3,508 pages were identified as likely containing structured data.
- **Cargo Usage:** Cargo tables are heavily used (`Relics`, `Relic_Resonance`). While direct `action=cargoquery` was not used in the final pass due to rate limiting safety, the raw wikitext contains the `{{CargoRelic ...}}` calls which are perfect for parsing.
- **Wikitext Quality:** Raw and lossless.
- **Markdown Quality:** Cleaned of major noise, suitable for RAG/LLM.

## First 10 Likely Item Pages
1. 1001 Nights
2. 100 Years of Solitude
3. 100 Years of Solitude+Hamlet
4. 100 Years of Solitude+Hamlet+Tain Bó Cúailnge+An Actor Prepares
5. 100 Years of Solitude+Mysterious Relic
6. 12 Zodiac Runes
7. 12 Zodiac Runes+Sierpinski Triangle
8. 1900's Vinyl Record
9. 1982 Wine
10. 1st Winter Oly. Poster+Chang'e and Laurel

## Gaps & Next Steps
- **Cargo Tables:** To get even cleaner data, we could specifically query the `Relics` table if we need a flat CSV.
- **Images:** Image files were skipped to save tokens and bandwidth.
- **Namespaces:** Only namespace 0 was pulled.

**Final Command:**
```bash
python3 supersnail_mediawiki_scrape.py \
  --api "https://supersnail.wiki.gg/api.php" \
  --output-dir ./dump \
  --namespaces 0 \
  --sleep-seconds 0.5 \
  --batch-size 10
```

**Output Folder:** `runs/supersnail_wiki_20260419_112749/dump`
