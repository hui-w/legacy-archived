<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html"/>
  <xsl:template match="/">
    <xsl:apply-templates select="Files"/>
  </xsl:template>
  <xsl:template match="Files">
    <html>
      <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
        <title>Files</title>
      </head>
      <body>
        <xsl:for-each select="File">
          <div>
            <xsl:value-of select="@Name"/>, <xsl:value-of select="@Length"/>
          </div>
        </xsl:for-each>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>