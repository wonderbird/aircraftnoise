package io.github.wonderbird.aircraftnoise.recorder.logic;

import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.List;

public class VersionFileReader {
    public String read() {
        try {
            List<String> lines = Files.readAllLines(Paths.get("VERSION"));
            return lines.get(0);
        } catch (Exception ex) {
            return "Exception: " + ex.getClass().getSimpleName();
        }
    }
}
