# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [2.2.1] - 2024-10-03

### Fixed

- Ensured the effect keeps working when camera post-processing is enabled by
  adjusting the render pass event.

## [2.2.0] - 2024-10-02

### Changed

- Migrated the effect to URP's render graph pipeline in place of the command
  buffer implementation.
