# Sources and historical interpretation

Retrieved and reviewed on 26 October 2026.

## Legacy Android application

Source: [hakanberat/DiveHub-Android-Legacy](https://github.com/hakanberat/DiveHub-Android-Legacy), main-branch archive downloaded into `.reference/`. No remote repository was changed, initialized, committed or pushed.

Inspected all 190 Java/XML files, drawable variants, launcher images, layouts, navigation activities, gallery arrays, course lists and the SQLite schema. The inventory is in `.reference/source_inventory.json`. Original reference files remain unmodified.

Migrated:

- The 12-field logbook concept, rebuilt with appropriate C#/SQLite types and an independent integer record identifier.
- All ten original named sites: Dive Hub Shore, Amphora Beach, Bogaz Ship Wreck, Alp, Bedri, Pitos, Noah's Ark, Ray Cave, Abdulselam and Amphora Wall.
- All 26 original gallery photographs and ten original site covers, copied once each.
- Original historical contact details from `res/layout/contact.xml`.
- Center background and team-list history, compared with the newer website.

The old five-second splash delay, SMTP/email implementation, embedded credentials, concatenated SQL delete operation, text-only numeric columns and destructive/broken upgrade statement were not migrated. No legacy Java code or reference files are bundled in the application.

## Official website

Sources: [home](https://dive-hub.com/en/), [about](https://dive-hub.com/en/about/), [team](https://dive-hub.com/en/about/our-team/), [sites](https://dive-hub.com/en/services/dive-sites/), [dives](https://dive-hub.com/en/services/dives/), [courses](https://dive-hub.com/en/courses/), [PADI](https://dive-hub.com/en/courses/padi-courses/), [CMAS](https://dive-hub.com/en/courses/cmas-courses/), [DAN](https://dive-hub.com/en/courses/dan-courses/), [TDI](https://dive-hub.com/en/courses/tdi-courses/) and their linked detail pages.

Accessible gallery, service, contact, about, team and all linked course pages were downloaded and read. The linked TDI pages outside the `/courses/` path were also inspected. Raw website responses remain in `.reference/website_*.html`; text extraction is in `.reference/text/`.

Imported:

- Website descriptions for the 12 currently listed site names. Four additions to the original catalog: Kocareis Beach, Balloon, Palm Beach and Tomofil Car Wreck.
- Eleven individual PADI program pages and six specialties: Digital Underwater Photography, Peak Performance Buoyancy, Deep Diver, Night Diver, Navigation and Boat Diver Specialty.
- CMAS 1*, 2** and 3***.
- DAN Oxygen Provider and Basic Life Support (BLS).
- Nine TDI individual or combined courses.
- Five service presentations: Try Dives, Shore Dives, Boat Dives, Night Dives and Guided Dives.
- Published historical background and five team names, roles and verified matching portraits.
- The original logo and six additional official photographs.

## Image inventory

48 authentic source assets: 36 from the legacy repository (26 gallery images + ten site covers), and 12 from the website (five portraits + one logo + six photographs).

32 images appear in the gallery. Seven additional SVG files in `Resources/Images` are locally authored navigation/branding/placeholder artwork. App icon and splash artwork are also local vectors. No unrelated stock image or generated bitmap is used.

[asset-manifest.json](asset-manifest.json) records each original asset's exact source URL. Original aspect ratios are preserved; covers intentionally crop with AspectFill and the full-screen viewer uses AspectFit. No duplicate file hashes were found among migrated images.

## Discrepancies and gaps

- Dive Hub Shore and Amphora Beach are preserved from 2014. The newer website does not list these names. They were not silently renamed to Kocareis Beach or Palm Beach.
- Amphora Wall and the website's A-Wall are treated together because the website itself uses both names in its description.
- Pitos has a reported seabed depth of 32 m, and A-Wall 38 m. Both website accounts also give a historical operational limit of 30 m. The cards show site depth; the detail pages explicitly explain the distinction.
- The generic course overview mislabels the TDI panel as CMAS. Verified TDI detail pages establish its correct category.
- The DAN BLS URL and source title contain the typo “bsaic”. The display title is corrected to Basic Life Support (BLS); the original URL is retained.
- The 2014 team list includes K. M. Hakan Koçdağ; the website instead includes Hakan Berat Dermican. They are not assumed to be the same person. Newer published profiles take precedence in the photographed team list.
- The English contact URL currently renders Turkish headings and empty information blocks. Historical telephone/address details therefore come from the 2014 app, with explicit historical labels. The Turkish contact page and online form were not embedded.
- No verified site-specific image was found for Kocareis Beach, Balloon, Palm Beach or Tomofil. A local ocean illustration is used and identified as such.
- Shore/boat access is unavailable for the four newer sites and Bogaz Ship Wreck where not explicitly established. No coordinates or other missing specifications were invented.
- Two TDI combined-course pages contain only a price or a price plus dive count. After omitting prices, missing descriptions/prerequisites are explicitly stated. The verified four-dive count is retained for Advanced Nitrox & Introduction to Tech Diving.
- Some archived course ages/depths appear outdated or inconsistent with modern expectations. They remain attributed historical statements, never presented as current training-agency standards. All old prices and booking claims are omitted.
- Original captions are unavailable for most gallery files. Captions identify the source/archive rather than inventing locations, people or species.
- Some legacy photographs are small/low resolution. They are preserved as authentic source media.

Initial browser fetches returned 502/timeouts. Direct public-page downloads succeeded after retry; the historical website also required a development-only TLS certificate-validation bypass because its certificate chain was untrusted on this machine. No such bypass exists in application code, and no network access is needed by the installed app.

Current operating status, live services and contact validity were not established. This application deliberately describes the center as historical.
