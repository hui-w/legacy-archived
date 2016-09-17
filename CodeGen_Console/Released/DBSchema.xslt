<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html"/>
  <xsl:variable name="tableCount" select="count(//table)"></xsl:variable>
  <xsl:template match="/">
    <xsl:apply-templates select="tables"/>
  </xsl:template>
  <xsl:template match="tables">
    <html>
      <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
        <title>Database Schema</title>
        <style type="text/css">
          div { padding: 0px 0px 30px 0px; }
          table { font-size: 12px; font-family: Verdana, Arial, Helvetica; }
          th { background: buttonface; }
          tr { background-color: #f3f3f3; }
          tr.aternate { background-color: #fefefe; }
        </style>
      </head>
      <body>
        <xsl:for-each select="table">
          <div>
            <h3>
              <xsl:value-of select="position()" /> of <xsl:copy-of select="$tableCount" />: 
	      <xsl:value-of select="@name"/> / <xsl:value-of select="@comment"/>
            </h3>
            <table>
              <tr>
                <th style="width: 30px;">#</th>
                <th style="width: 150px;">Name</th>
                <th style="width: 50px;">Type</th>
                <th style="width: 50px;">Size</th>
                <th style="width: 60px;">Primary</th>
                <th style="width: 300px;">Comment</th>
                <th style="width: 60px;">Relation</th>
              </tr>
              <xsl:apply-templates select="column"/>
            </table>
          </div>
        </xsl:for-each>
      </body>
    </html>
  </xsl:template>
  <xsl:template match="column">
    <tr>
      <xsl:if test="position() mod 2=0">
        <xsl:attribute name="class">aternate</xsl:attribute>
      </xsl:if>
      <td style="text-align: center;">
        <xsl:value-of select="position()" />
      </td>
      <td>
        <xsl:value-of select="@name"/>
      </td>
      <td>
        <xsl:value-of select="@dataType"/>
      </td>
      <td style="text-align: center;">
        <xsl:value-of select="@fieldSize"/>
      </td>
      <td style="text-align: center;">
        <xsl:value-of select="@isPrimary"/>
      </td>
      <td>
        <xsl:value-of select="@comment"/>
      </td>
      <td style="text-align: center;">
        <xsl:if test="@relation != ''">
          <span style="cursor: pointer;">
            <xsl:attribute name="title">
              <xsl:value-of select="@relation"/>
            </xsl:attribute>
            True
          </span>
          </xsl:if>
      </td>
    </tr>
  </xsl:template>
</xsl:stylesheet>